using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeathBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 0f;
        agent.SetDestination(agent.transform.position); 
        agent.enabled = false;
        animator.SetBool("isDead",false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.GetComponent<EnemyManager>().CloseDamageCollider();
    }
}
