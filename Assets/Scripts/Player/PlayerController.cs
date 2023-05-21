using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float TargetSmoothAnimation = 2;
    public float CycloneDuration = 5f;
    public float CycloneDelay = 5f;
    private float tempCycloneDelay = 0f;


    private string nextLevelName;


    public GameObject SlashParticle;
    public GameObject CycloneParticle;
    public GameObject CycloneTextButton;
    public GameObject UseButton;
    private Vector2 joystickMove;
    private Vector2 mouseLook;
    private Vector2 joystickLook;
    [HideInInspector] private Animator animator;


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
        if (animator.GetBool("isCyclone") == false)
        {
            animator.SetBool("isAttack", true);
        }
    }

    public void OnCyclone(InputAction.CallbackContext context)
    {
        if (Time.time - tempCycloneDelay >= CycloneDelay)
        {
            tempCycloneDelay = Time.time;
            animator.SetBool("isCyclone", true);
            CycloneParticle.SetActive(true);
            animator.SetLayerWeight(2, 1);
            StartCoroutine(CycloneCoroutine());
        }
    }

    public void OnUsePortal(InputAction.CallbackContext context)
    {
        OnTeleportToLocation();
    }

    public void OnDash(InputAction.CallbackContext context)
    {

    }

    IEnumerator CycloneCoroutine()
    {
        yield return new WaitForSeconds(CycloneDuration);
        animator.SetBool("isCyclone", false);
        CycloneParticle.SetActive(false);
        animator.SetLayerWeight(2, 0);
        transform.GetComponent<PlayerManager>().CloseDamageCollider();

    }


    


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SlashParticle.SetActive(false);
        CycloneParticle.SetActive(false);

        aimDirection = new Vector3(0f, 0f, 1f);

        _joyStickMoveAngle = Mathf.Atan2(joystickMove.x, joystickMove.y) * Mathf.Rad2Deg;
        _joyStickLookAngle = Mathf.Atan2(joystickLook.x, joystickLook.y) * Mathf.Rad2Deg;

        _joySticksAngle = _joyStickLookAngle - _joyStickMoveAngle;
        _lastJoystickLookAngle = _joyStickLookAngle;

        tempCycloneDelay -= CycloneDelay;
    }


    void Update()
    {
        if (Time.time - tempCycloneDelay >= CycloneDelay)
        {
            CycloneTextButton.GetComponent<TextMeshProUGUI>().text = "Cyclone";
        }
        else
        {
            CycloneTextButton.GetComponent<TextMeshProUGUI>().text = Convert.ToString((CycloneDelay - Mathf.Round(Time.time - tempCycloneDelay)));
        }

        if (animator.GetBool("isCyclone") == false)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                animator.SetBool("isAttack", false);
            }
            if ((joystickLook.x == 0) || (joystickLook.y == 0))
            {
                animator.SetBool("isAttack", false);
            }
            else
            {
                animator.SetBool("isAttack", true);
            }
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

        StickMoveAnimation();
    }

    void FixedUpdate()
    {
        if (joystickLook.x == 0 && joystickLook.y == 0)
        {
            movePlayer();
        }
        else
        {
            movePlayerWithAim();
        }
    }


    public void movePlayer()
    {
        movement = new Vector3(joystickMove.x, 0f, joystickMove.y);

        if (movement == Vector3.zero)
        {
            animator.SetFloat("MoveSpeed", 0);
        }
        if (movement != Vector3.zero && (joystickLook.x == 0 && joystickLook.y == 0))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        _lastJoystickLookAngle = Mathf.Atan2(joystickMove.x, joystickMove.y) * Mathf.Rad2Deg;

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

    }

    public void movePlayerWithAim()
    {
        aimDirection = new Vector3(joystickLook.x, 0f, joystickLook.y);

        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.15f);
        }

        movement = new Vector3(joystickMove.x, 0f, joystickMove.y);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void StickMoveAnimation()
    {
        _joyStickMoveAngle = Mathf.Atan2(joystickMove.x, joystickMove.y) * Mathf.Rad2Deg;
        _joyStickLookAngle = Mathf.Atan2(joystickLook.x, joystickLook.y) * Mathf.Rad2Deg;


        _joyStickLookAngle = CheckAngle(_joyStickLookAngle);
        _joyStickMoveAngle = CheckAngle(_joyStickMoveAngle);

        if (!(joystickLook.x == 0) || !(joystickLook.y == 0))
        {
            _lastJoystickLookAngle = _joyStickLookAngle;
        }

        _joySticksAngle = _lastJoystickLookAngle - _joyStickMoveAngle;

        _joySticksAngle = CheckAngle(_joySticksAngle);

        if (_finalAngle > 345f || _finalAngle < 25f)
        {
            // _finalAngle = Mathf.Lerp(_finalAngle, _joySticksAngle, Time.deltaTime * TargetSmoothAnimation);
            _finalAngle = _joySticksAngle;
            animator.SetFloat("StickMoveAngle", _joySticksAngle);
        }
        else
        {
            _finalAngle = Mathf.Lerp(_finalAngle, _joySticksAngle, Time.deltaTime * TargetSmoothAnimation);

            animator.SetFloat("StickMoveAngle", _joySticksAngle);
        }
    }
    private float CheckAngle(float JoyStickAngle)
    {
        if (JoyStickAngle <= 0f)
        {
            JoyStickAngle += 360f;
        }
        return JoyStickAngle;
    }

    public void EnableSlashParticle()
    {
        SlashParticle.SetActive(true);
    }
    public void DisableSlashParticle()
    {
        SlashParticle.SetActive(false);
    }

    public void OnTeleportToLocation()
    {
        SceneManager.LoadScene(nextLevelName);
    }
    private void OnTriggerEnter(Collider other) 
    {
        try
        {
            nextLevelName =  other.GetComponent<TeleportScript>().toLevelName;
        }
        catch
        {

        }
    }
    private void OnTriggerExit(Collider other) 
    {
        try
        {
            nextLevelName = "";
        }
        catch
        {

        }
    }
}
