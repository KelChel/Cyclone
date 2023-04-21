using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float TargetSmoothAnimation = 2;
    private Vector2 joystickMove;
    private Vector2 mouseLook;
    private Vector2 joystickLook;
    private Vector3 rotationTarget;
    [HideInInspector]private Animator animator;


    Vector3 aimDirection;
    Vector3 movement;

    float _joyStickMoveAngle;
    float _joyStickLookAngle;
    float _joySticksAngle;
    float _lastJoystickLookAngle;

    float _finalAngle = 0;

    public void OnMove(InputAction.CallbackContext context)
    {
        
        joystickMove = context.ReadValue<Vector2>();
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
        animator.SetBool("isAttack", true);
    }


    void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Start() {
        aimDirection = new Vector3(0f, 0f, 1f);
        _joyStickMoveAngle = Mathf.Atan2(joystickMove.x, joystickMove.y)*Mathf.Rad2Deg;
        _joyStickLookAngle = Mathf.Atan2(joystickLook.x, joystickLook.y)*Mathf.Rad2Deg;
        _joySticksAngle = _joyStickLookAngle - _joyStickMoveAngle;
        _lastJoystickLookAngle = _joyStickLookAngle;
    }


    void Update()
    {
        // Debug.Log("X = " + joystickLook.x);
        // Debug.Log("Y = " + joystickLook.y);
        // Debug.Log(Mathf.Atan2(joystickLook.x, joystickLook.y)*Mathf.Rad2Deg);

        if((joystickLook.x == 0) || (joystickLook.y == 0))
        {
            animator.SetBool("isAttack", false);
        }
        else
        {
            animator.SetBool("isAttack", true);
        }


        if (joystickMove == Vector2.zero)
        {
            animator.SetFloat("MoveSpeed", 0f);
            
        }
        else if (Math.Abs(joystickMove.x) > 0.7 || Math.Abs(joystickMove.y) > 0.7)
        {
            animator.SetFloat("MoveSpeed", 1f);
        }
        else
        {
            animator.SetFloat("MoveSpeed", 0.5f);
        }
        

    }

     



    void FixedUpdate()
    {
        
        if (joystickLook.x == 0 && joystickLook.y == 0)
        {
            // animator.SetBool("isCyclone", false);
            movePlayer();
        }
        else
        {
            // animator.SetBool("isAttack", false);
            // animator.SetBool("isCyclone", true);
            movePlayerWithAim();
        }
        StickMoveAnimation();
    }


    public void movePlayer()
    {
        movement = new Vector3(joystickMove.x, 0f, joystickMove.y);
        
        // Vector3 movement = new Vector3(1f, 0f, 1f);
        if (movement == Vector3.zero)
        {
            animator.SetFloat("MoveSpeed", 0);
        }
        // if (movement != Vector3.zero && (joystickLook.x == 0 && joystickLook.y == 0))
        // {
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        //     _lastJoystickLookAngle = _joyStickMoveAngle;
        // }
        
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        
    }
    public void movePlayerWithAim()
    {
        
        aimDirection = new Vector3(joystickLook.x, 0f, joystickLook.y);

        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.15f);
        }
        

        // Vector3 movement = new Vector3(joystickMove.x, 0f, joystickMove.y);
        movement = new Vector3(joystickMove.x, 0f, joystickMove.y);
        // transform.Translate(movement * speed * Time.deltaTime);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

    }

    public void StickMoveAnimation()
    {
        _joyStickMoveAngle = Mathf.Atan2(joystickMove.x, joystickMove.y)*Mathf.Rad2Deg;
        _joyStickLookAngle = Mathf.Atan2(joystickLook.x, joystickLook.y)*Mathf.Rad2Deg;

        
        _joyStickLookAngle = CheckAngle(_joyStickLookAngle);
        _joyStickMoveAngle = CheckAngle(_joyStickMoveAngle);

        if(!(joystickLook.x == 0) || !(joystickLook.y == 0))
        {
            _lastJoystickLookAngle = _joyStickLookAngle;
        }
        
        _joySticksAngle = _lastJoystickLookAngle - _joyStickMoveAngle;

        _joySticksAngle = CheckAngle(_joySticksAngle);
        
        
        // _finalAngle = Mathf.Lerp(_finalAngle, _joySticksAngle, Time.deltaTime * TargetSmoothAnimation);
        // Debug.Log(_finalAngle);
        if(joystickMove.x == 0 && joystickMove.y == 0)
        {
            // _finalAngle = Mathf.Lerp(_finalAngle, _lastJoystickLookAngle, Time.deltaTime * TargetSmoothAnimation);
            animator.SetFloat("StickMoveAngle", 0);
        }
        else
        {
            // _finalAngle = Mathf.Lerp(_finalAngle, _lastJoystickLookAngle, Time.deltaTime * TargetSmoothAnimation);
            animator.SetFloat("StickMoveAngle", _joySticksAngle);
        }
        // animator.SetFloat("StickMoveAngle", _finalAngle);

        // else if(_joySticksAngle <=22.5 || _joySticksAngle > 337.5)
        // {
        //     animator.SetInteger("StickMoveAngle", 1);
        // }
        // else if(_joySticksAngle <=67.5 && _joySticksAngle > 22.5)
        // {
        //     animator.SetInteger("StickMoveAngle", 2);
        // }
        // else if(_joySticksAngle <=112.5 && _joySticksAngle > 67.5)
        // {
        //     animator.SetInteger("StickMoveAngle", 3);
        // }
        // else if(_joySticksAngle <=157.5 && _joySticksAngle > 112.5)
        // {
        //     animator.SetInteger("StickMoveAngle", 4);
        // }
        // else if(_joySticksAngle <=360-22.5 && _joySticksAngle > 360-67.5)
        // {
        //     animator.SetInteger("StickMoveAngle", -2);
        // }
        // else if(_joySticksAngle <=360-67.5 && _joySticksAngle > 360-112.5)
        // {
        //     animator.SetInteger("StickMoveAngle", -3);
        // }
        // else if(_joySticksAngle <=360-112.5 && _joySticksAngle > 360-157.5)
        // {
        //     animator.SetInteger("StickMoveAngle", -4);
        // }
        // else if(_joySticksAngle > 157.5 && _joySticksAngle <= 360-157.5)
        // {
        //     animator.SetInteger("StickMoveAngle", 5);
        // }
    }
    private float CheckAngle(float JoyStickAngle)
    {
        if(JoyStickAngle <= 0f)
        {
            JoyStickAngle += 360f;
        }
        return JoyStickAngle;
    }
}
