using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
 
    void Update()
    {
        // if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        // {
        //     animator.SetBool("isAttack", false);
        // }
        // if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && !animator.GetCurrentAnimatorStateInfo(0).IsName("isDanceAttack"))
        // {
        //     animator.SetBool("isCyclone", false);
        // }
    }

    public void DefaultAttack()
    { 
        animator.SetBool("isAttack", true);
    }

    public void SpinAttack()
    {
        animator.SetBool("isCyclone", true);
    }
}
