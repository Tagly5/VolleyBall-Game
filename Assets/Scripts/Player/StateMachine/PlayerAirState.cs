using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerAirState : PlayerBaseState
{
    
    private float jumpStartTime;
    public override void Enter()
    {
        jumpStartTime = ctxMachine.playerData.jumpTime;
        // Debug.Log("AIRBORNE");
        ctxMachine = gameObject.GetComponentInParent<PlayerStateMachine>();
    }
    public override void Do()
    {
        ctxMachine.playerData.jumpTime -= Time.deltaTime;
    
        CheckSwitchState(this);
        SubState(ctxMachine.subState);
        if(ctxMachine.playerData.jumpTime > 0 && ctxMachine.GetyInput() == 1)
        {
            ctxMachine.body.velocity = new Vector2(ctxMachine.body.velocity.x, ctxMachine.playerData.jumpForce);
        }
        if(ctxMachine.playerData.jumpTime <= 0 || (ctxMachine.GetyInput() == 0 && ctxMachine.body.velocity.y > 0))
        {
            ctxMachine.playerData.jumpTime = 0;
        }
    }
    public override void FixedDo()
    {

    }
    public override void Exit(PlayerBaseState newState)
    {
        ctxMachine.playerData.jumpTime = jumpStartTime;

        ctxMachine.superState = newState;
        newState.Enter();
    }
    public override void CheckSwitchState(PlayerBaseState actualState)
    {
        if(ctxMachine.grounded && ctxMachine.body.velocity.y < 0.01f)
        {
            PlayerBaseState newState = player.GetComponentInChildren<PlayerGroundState>();
            actualState.Exit(newState);
        }
        
    }
}
