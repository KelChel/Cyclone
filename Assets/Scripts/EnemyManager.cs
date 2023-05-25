using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour, IDamagable
{
    private DamageScript damageScript;
    [HideInInspector] public Animator animator;
    private NavMeshAgent navMeshAgent;

    public int enemyDamage = 10;
    public int healthEnemy = 50;
    [HideInInspector] public int maxHealth;

    public bool isAlive = true;

    public GameObject Buff;

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
        healthEnemy -= damageAmount;
        if (healthEnemy <= 0)
        {
            EnemyDied();
        }
    }

    public void EnemyDied()
    {
        healthEnemy = 0;

        animator.SetBool("isPatrolling", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
        GetComponent<Collider>().enabled = false;
        isAlive = false;
    }

    public void DropBuff()
    {
        Instantiate(Buff, transform);
    }
}
