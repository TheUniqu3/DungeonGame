using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] private ZombieStats ZombieStats;

    [SerializeField] public GameObject Cake;
    [SerializeField] private GameObject Victory;

    [SerializeField] private GameObject target;

    private bool objectIsSpawned = false;

    private void Start()
    {
        ZombieStats = gameObject.GetComponent<ZombieStats>();
        Victory = GameObject.FindGameObjectWithTag("Prize");
        target = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        if (ZombieStats.BossIsDead())
        {
            if(!objectIsSpawned)
                SpawnCake();
            StartCoroutine(EndTheGame());
        }
    }

    private void SpawnCake()
    {
        Instantiate(Cake, gameObject.transform.position, Quaternion.identity);
        objectIsSpawned = true;
    }

    private IEnumerator EndTheGame()
    {
        yield return new WaitForSeconds(5);
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        targetStats.TakeDamage(400);

    }
}
