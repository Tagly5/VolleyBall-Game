using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public override void Enter()
    {
        // Debug.Log("RUN");
        contextStateMachine = gameObject.GetComponentInParent<PlayerStateMachine>();
    }
    public override void Do()
    {
        CheckSwitchState(this);
    }
    public override void FixedDo()
    {
        MoveWithInput();

    }
    public override void Exit(PlayerBaseState newSubState)
    {
        contextStateMachine.subState = newSubState;
        newSubState.Enter();
    }
    public override void CheckSwitchState(PlayerBaseState actualSubState)
    {
        if(Mathf.Abs(contextStateMachine.body.velocity.x) < 0.05f && Math.Abs(contextStateMachine.GetxInput()) < 1)
        {
            PlayerBaseState newSubState = player.GetComponentInChildren<PlayerIdleState>();
            newSubState.Enter();
            actualSubState.Exit(newSubState);
        }
    }
    private void MoveWithInput()
    {
        if(Mathf.Abs(contextStateMachine.GetxInput()) > 0)
        {
            float increment = contextStateMachine.GetxInput() * contextStateMachine.acceleration;
            float newSpeed = Mathf.Clamp(contextStateMachine.body.velocity.x + increment, -contextStateMachine.groundSpeed, contextStateMachine.groundSpeed);
            contextStateMachine.body.velocity = new Vector2(newSpeed, contextStateMachine.body.velocity.y) ;

        }
        
    }
}
