using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour , IDamagable
{
    public DamageScript damageScript;
    [HideInInspector]public Animator animator;
    NavMeshAgent navMeshAgent;

    public int healthEnemy = 50;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

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
        animator.SetBool("isDead",true);
        animator.SetBool("isPatrolling", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);
    }
}
