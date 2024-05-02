using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerData_SO playerData;
    public PlayerBaseState superState;
    public PlayerBaseState subState;

    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    private float xInput;
    private float yInput;
    private float atkInput;

    [SerializeField] private Vector2 aimDirection;
    public bool grounded;


    #region Getters and Setters
    public float GetyInput() => yInput;
    public float GetxInput() => xInput;
    public float GetAtkInput() => atkInput;
    public Vector2 GetAimDirection() => aimDirection;
    public void SetAimDirection(float valueX, float valueY) => aimDirection = new Vector2(valueX, valueY);

    #endregion

    void Start()
    {
        superState = gameObject.GetComponentInChildren<PlayerGroundState>();
        subState = gameObject.GetComponentInChildren<PlayerIdleState>();
        superState.Enter();
        subState.Enter();

    }
    void Update()
    {

        superState.Do();
        subState.Do();
    }


    public void FixedUpdate()
    {
        superState.FixedDo();
        subState.FixedDo();
        CheckGround();
        ApplyFriction();
    }
    
    public void ApplyFriction()
    {
        if(grounded && xInput == 0 )
        {
            body.velocity *= playerData.groundDecay;
        }
    }

    public void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max,groundMask).Length > 0;
    }

    public void OnRun(InputAction.CallbackContext context){
        if(context.performed)
        {
            xInput = context.ReadValue<float>();

        }
        if(context.canceled)
        {
            xInput = context.ReadValue<float>();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            // Debug.Log("Jumping...");
            yInput = context.ReadValue<float>();
        }
        if(context.canceled)
        {
            yInput = context.ReadValue<float>();
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            // Debug.Log("Attacking...");
            atkInput = context.ReadValue<float>();
        }
        if(context.canceled)
        {
            atkInput = context.ReadValue<float>();
        }
    }
}
