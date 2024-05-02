using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public override void Enter()
    {
        // Debug.Log("GROUND");
        ctxMachine = gameObject.GetComponentInParent<PlayerStateMachine>();
    }
    public override void Do()
    {
        CheckSwitchState(this);
        SubState(ctxMachine.subState);

        if(ctxMachine.subState = player.GetComponentInChildren<PlayerIdleState>())
        {
            //Manchete
            ctxMachine.SetAimDirection(0.01f, 1f);
        }
    }
    public override void FixedDo()
    {

    }
    public override void SubState(PlayerBaseState actualSubState)
    {
        if(Mathf.Abs(ctxMachine.body.velocity.x) > 0.01f && actualSubState != player.GetComponentInChildren<PlayerRunningState>() )
        {
            PlayerBaseState newSubState = player.GetComponentInChildren<PlayerRunningState>();
            newSubState.Enter();
            ctxMachine.subState = newSubState;
        }
    }
    public override void Exit(PlayerBaseState newState)
    {
        ctxMachine.superState = newState;
        newState.Enter();
    }
    public override void CheckSwitchState(PlayerBaseState actualState)
    {
        if(ctxMachine.GetyInput() == 1 || !ctxMachine.grounded)
        {
            
            PlayerBaseState newState = player.GetComponentInChildren<PlayerAirState>();
            actualState.Exit(newState);
            
        }
        
    }
}
