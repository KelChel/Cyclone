using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    private Vector2 mouseLook;
    private Vector2 joystickLook;
    private Vector3 rotationTarget;
    public Animator animator;


    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        playerAttack();
    }


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (joystickLook.x == 0 && joystickLook.y == 0)
        {
            animator.SetBool("isDanceAttack", false);
        }
        else
        {
            animator.SetBool("isAttack", false);
            animator.SetBool("isDanceAttack", true);
        }
        movePlayer();


        if (move == Vector2.zero)
        {
            animator.SetFloat("MoveSpeed", 0f);
        }
        else if (Math.Abs(move.x) > 0.7 | Math.Abs(move.y) > 0.7)
        {
            animator.SetFloat("MoveSpeed", 1f);
        }
        else
        {
            animator.SetFloat("MoveSpeed", 0.5f);
        }
    }

    public void playerAttack()
    {
        animator.SetBool("isAttack", true);
    }

    public void playerSpinAttack()
    {
        animator.SetBool("isDanceAttack", true);
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        if (movement == Vector3.zero)
        {
            animator.SetFloat("MoveSpeed", 0);
        }
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
