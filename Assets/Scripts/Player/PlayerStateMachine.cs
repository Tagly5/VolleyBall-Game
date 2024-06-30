using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerBaseState superState;
    public PlayerBaseState subState;

    public Rigidbody2D body;
    public CircleCollider2D groundCheck;
    public float groundRadius;
    public LayerMask groundMask;
    public float groundSpeed;
    public float acceleration;
    [Range(0, 1f)] public float groundDecay;

    private float xInput;
    private float yInput;
    private float atkInput;
    public float jumpForce;
    public bool grounded;

    #region Getters and Setters
    public float GetyInput() => yInput;
    public float GetxInput() => xInput;

    public float GetJumpForce() => jumpForce;

    #endregion

    void Start()
    {
        superState = gameObject.GetComponentInChildren<PlayerGroundState>();
        // subState = gameObject.GetComponentInChildren<PlayerIdleState>();
        superState.Enter();
        // subState.Enter();

    }
    void Update()
    {
        superState.Do();
        // subState.Do();


    }


    public void FixedUpdate()
    {
        superState.FixedDo();
        // subState.FixedDo();
        CheckGround();
        ApplyFriction();
    }

    public void ApplyFriction()
    {
        if (grounded && xInput == 0)
        {
            body.velocity *= groundDecay;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundRadius);
    }
    public void CheckGround()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, groundMask);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            xInput = context.ReadValue<float>();

        }
        if (context.canceled)
        {
            xInput = context.ReadValue<float>();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("Jumping...");
            yInput = context.ReadValue<float>();
        }
        if (context.canceled)
        {
            yInput = context.ReadValue<float>();
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("Attacking...");
            atkInput = context.ReadValue<float>();
        }
        if (context.canceled)
        {
            atkInput = context.ReadValue<float>();
        }
    }


}
