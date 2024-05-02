using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public override void Enter()
    {
        // Debug.Log("GROUND");
        contextStateMachine = gameObject.GetComponentInParent<PlayerStateMachine>();
    }
    public override void Do()
    {
        CheckSwitchState(this);
        SubState(contextStateMachine.subState);
    }
    public override void FixedDo()
    {

    }
    public override void SubState(PlayerBaseState actualSubState)
    {
        if(Mathf.Abs(contextStateMachine.body.velocity.x) > 0.01f && actualSubState != player.GetComponentInChildren<PlayerRunningState>() )
        {
            PlayerBaseState newSubState = player.GetComponentInChildren<PlayerRunningState>();
            newSubState.Enter();
            contextStateMachine.subState = newSubState;
        }
    }
    public override void Exit(PlayerBaseState newState)
    {
        contextStateMachine.superState = newState;
        newState.Enter();
    }
    public override void CheckSwitchState(PlayerBaseState actualState)
    {
        if(contextStateMachine.GetyInput() == 1 || !contextStateMachine.grounded)
        {
            
            PlayerBaseState newState = player.GetComponentInChildren<PlayerAirState>();
            actualState.Exit(newState);
            
        }
        
    }
}
