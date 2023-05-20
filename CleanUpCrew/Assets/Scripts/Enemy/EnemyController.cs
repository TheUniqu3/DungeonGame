using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Attacking Sound")]
    [SerializeField] private AudioClip attack;

    private NavMeshAgent agent; 
    private Animator anim;
    private ZombieStats stats;
    private PlayerHUD playerHUD;
    private SoundManager sound;
    [Header("target")]
    [SerializeField] private GameObject target;
    [SerializeField] private float stoppingDistance;

    [Header("LayerMask EnemyNav")]
    public LayerMask playerLayerMask , groundLayerMask;

    [Header("Enemy Walking")]
    private Vector3 walkPoint;
    private bool walkPointSet = false;
    private float walkPointRange = 10;
    private float timeOfWalk = 0;
    public float sightRange;
    public bool playerInSightRange;

    [Header("Enemy Attacking")]
    [SerializeField] private float timeOfLastAttack = 0f;
    [SerializeField] private bool hasStopped = false;

    private bool DieAnimation = false;

    private void Start()
    {
        GetReferences();
        playerInSightRange = false;
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayerMask);

        if (DieAnimation) StartCoroutine(CharcterDieAnimation());
        if (!playerInSightRange && !DieAnimation) Patroling();
        if (playerInSightRange && !DieAnimation) MoveToTarget();
       
    }
    private void Patroling()
    {
        if (Time.time > timeOfWalk + 5f)
        {
            walkPointSet = false;
        }
        if (!walkPointSet)
        {
            anim.SetFloat("Speed", 0f);
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(gameObject.transform.position.x + randomX,0, gameObject.transform.position.z + randomZ);
        walkPointSet = true;
        timeOfWalk = Time.time;

    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.transform.position);

        anim.SetFloat("Speed", 1f, 0.3f,Time.deltaTime);

        RotateToTarget();

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if(distanceToTarget <= stoppingDistance)  
        {
            anim.SetFloat("Speed", 0f);

            if (!hasStopped)
            {
                hasStopped= true;
                timeOfLastAttack = Time.time - 1.0f;
            }
           
            
            if(Time.time >= timeOfLastAttack + stats.attackSpeed)
            {
                timeOfLastAttack = Time.time;
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                AttackTarget(targetStats);
                playerHUD.UpdatescoreUI(-30);
            }
        }
        else
        {
            anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
            if (hasStopped) 
            {
                hasStopped = false;
            }
        }

    }
    private void RotateToTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion rotation =Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = rotation;
    }
    public void SetDeath()
    {
        DieAnimation = true;
    }

    private void AttackTarget(CharacterStats statsToDamage)
    {
        sound.PlaysoundEffect(attack);
        anim.SetTrigger("Attack");
        stats.DealDamage(statsToDamage);
    }

    private IEnumerator CharcterDieAnimation()
    {
        anim.SetTrigger("IsDead");
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<ZombieStats>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerHUD = target.GetComponent<PlayerHUD>();
        sound = target.GetComponent<SoundManager>();
    }
   
}
