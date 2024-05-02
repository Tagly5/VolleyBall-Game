using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerAirState : PlayerBaseState
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpStartTime;
    public override void Enter()
    {
        jumpStartTime = jumpTime;
        // Debug.Log("AIRBORNE");
        contextStateMachine = gameObject.GetComponentInParent<PlayerStateMachine>();
    }
    public override void Do()
    {
        jumpTime -= Time.deltaTime;
    
        CheckSwitchState(this);
        SubState(contextStateMachine.subState);
        if(jumpTime > 0 && contextStateMachine.GetyInput() == 1)
        {
            contextStateMachine.body.velocity = new Vector2(contextStateMachine.body.velocity.x, jumpForce);
        }
        if(jumpTime <= 0 || (contextStateMachine.GetyInput() == 0 && contextStateMachine.body.velocity.y > 0))
        {
            jumpTime = 0;
        }
    }
    public override void FixedDo()
    {

    }
    public override void Exit(PlayerBaseState newState)
    {
        jumpTime = jumpStartTime;

        contextStateMachine.superState = newState;
        newState.Enter();
    }
    public override void CheckSwitchState(PlayerBaseState actualState)
    {
        
    }
}
