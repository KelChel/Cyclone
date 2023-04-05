using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    //
public float cooldownTime = 0.8f;
private float nextFireTime = 0f;
public static int noOfClicks = 0;
float lastClickedTime = 0;
float maxComboDelay = 1;
    //

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && !animator.GetCurrentAnimatorStateInfo(0).IsName("isAttack"))
        {
            animator.SetBool("isAttack", false);
            noOfClicks = 0;
        }

        if(Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        //if(Time.time > nextFireTime)
        //{
        //    if(Input.GetMouseButtonDown(0))
        //    {
        //        defaultAttack();
        //    }
        //}
    }

    public void defaultAttack()
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("isAttack"));
        lastClickedTime = Time.time;
        noOfClicks++;
        if(noOfClicks == 1)
        {
            animator.SetBool("isAttack", true);
        }
    }
}
