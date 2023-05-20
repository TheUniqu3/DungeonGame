using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected bool isDead;

    
    public virtual void CheckHealth()
    {
        if(health <= 0)
        {
            health= 0;
            Die();
        }
        if(health >= maxHealth) 
        {
            health= maxHealth;
        }
    }
    public virtual void Die()
    {
        isDead = true;
    }

    public void SetHealthTo(int healthToSet)
    {
        health = healthToSet;
        CheckHealth();
    }
    public void TakeDamage(int damage)
    {
        int healthAfterDamage = health - damage;
        SetHealthTo(healthAfterDamage);
    }
    public void Heal(int heal) 
    { 
        int healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public virtual void InitVariables()
    {
        maxHealth = 100;
        SetHealthTo(maxHealth);
        isDead = false;
    }

}
