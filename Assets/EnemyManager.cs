using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour , IDamagable
{
    private DamageScript damageScript;
    [HideInInspector]public Animator animator;
    private NavMeshAgent navMeshAgent;
    
    public int enemyDamage = 10;
    public int healthEnemy = 50;
    [HideInInspector]public int maxHealth;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        damageScript = GetComponentInChildren<DamageScript>();
    }
    private void Start() 
    {
        maxHealth = healthEnemy;
        damageScript.damageAmount = enemyDamage;
    }

    public void OpenDamageCollider()
    {
        damageScript.EnableDamageCollider();
    }
    public void CloseDamageCollider()
    {
        damageScript.DisableDamageCollider();
    }

    public void TakeDamage(int damageAmount)
    {
        if(healthEnemy > 0)
        {
            healthEnemy -= damageAmount;
            if(healthEnemy <= 0)
            {
                EnemyDied();
            }
        } 
        else
        {
            EnemyDied();
        }
    }

    public void EnemyDied()
    {
        healthEnemy = 0;
        GetComponent<Collider>().enabled = false;
        CloseDamageCollider();
        animator.SetBool("isDead",true);
        animator.SetBool("isPatrolling", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);
    }
}
