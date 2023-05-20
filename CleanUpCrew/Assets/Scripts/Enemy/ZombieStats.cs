using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ZombieStats : CharacterStats
{
    private EnemyController controller;
    private GameObject player;
    private PlayerHUD playerHUD;

    [Header("Enemy Combat")]
    [SerializeField] private int damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] private bool canAttack;

    [Header("Enemy Health")]
    [SerializeField] private int CharcterHealth = 100;

    [Header("Enemy Damage")]
    [SerializeField] private int enemyDamage = 10;


    private float timeDelay;
    private void Start()
    {
        InitVariables();
        GetReference();
    }

    public void DealDamage(CharacterStats statsToDamage)
    {
        statsToDamage.TakeDamage(damage);
        playerHUD.UpdatescoreUI(10);
    }

    public override void Die()
    {
        base.Die();
        playerHUD.UpdatescoreUI(50);
        playerHUD.UpdateScoreEnemyKilled();
        controller.SetDeath();
    }
    public bool BossIsDead()
    {
        return isDead;
    }
    public override void InitVariables()
    {   
        maxHealth = CharcterHealth;
        SetHealthTo(maxHealth);
        isDead = false;
        damage = enemyDamage;
        attackSpeed= 2f;
        canAttack = false;
    }
    private void GetReference()
    {
        controller = GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHUD = player.GetComponent<PlayerHUD>();
    }
}
