using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    //[SerializeField] private VariableJoystick joystick;
    //[SerializeField] private Animator animator;

    [SerializeField] private float speed = 40f;

    private float horizontalMove;
    private bool wasMoving;
    private bool jump = false;
    private bool jumpingOff = false;
    private bool crouch = false;
    private bool run = false;
    private bool deceleration = false;

    public UnityEvent OnStopEvent;
    public UnityEvent OnMoveEvent;

    private void Start()
    {
        GameEvents.current.onDeceleration += ChangeState;
        //joystick = FindObjectOfType<VariableJoystick>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        //horizontalMove = joystick.Horizontal * speed;

        if (horizontalMove == 0)
        {
            if (wasMoving)
            {
                OnStopEvent?.Invoke();
                wasMoving = false;
            }
        }
        else
        {
            if (!wasMoving)
            {
                OnMoveEvent?.Invoke();
                wasMoving = true;
            }
        }

        //if (Input.GetKey(runBotton) && !crouch)
        //{
        //    horizontalMove *= runFactor;
        //}

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            //animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Run"))
        {
            run = true;
        } 
        else if (Input.GetButtonUp("Run"))
        {
            run = false;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            //animator.SetBool("IsCrouching", true);
        } 
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            //animator.SetBool("IsCrouching", false);
        }

        if (Input.GetButtonDown("JumpingOff"))
        {
            jumpingOff = true;
            //crouch = true;
        }
        //else if (Input.GetButtonUp("JumpingOff"))
        //{
        //    jumpingOff = false;
        //}
        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jumpingOff, deceleration, run, jump);
        jumpingOff = jump = false;
        //animator.SetBool("IsJumping", false);
    }

    private void OnDisable()
    {
        //Debug.Log("OnDisable PlayerMovement");
        GameEvents.current.onDeceleration -= ChangeState;
        horizontalMove = 0f;
        controller.Move(0f, false, false, false, false, false);
    }

    private void ChangeState(bool isStarted)
    {
        //Debug.Log(isStarted);
        deceleration = isStarted;
    }
}
