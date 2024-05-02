using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public override void Enter()
    {
        // Debug.Log("RUN");
        ctxMachine = gameObject.GetComponentInParent<PlayerStateMachine>();
    }
    public override void Do()
    {
        CheckSwitchState(this);
        CutAttack();
    }
    public override void FixedDo()
    {
        MoveWithInput();

    }
    public override void Exit(PlayerBaseState newSubState)
    {
        ctxMachine.subState = newSubState;
        newSubState.Enter();
    }
    public override void CheckSwitchState(PlayerBaseState actualSubState)
    {
        if(Mathf.Abs(ctxMachine.body.velocity.x) < 0.01f && Math.Abs(ctxMachine.GetxInput()) < 1)
        {
            PlayerBaseState newSubState = player.GetComponentInChildren<PlayerIdleState>();
            newSubState.Enter();
            actualSubState.Exit(newSubState);
        }
    }
    private void MoveWithInput()
    {
        if(Mathf.Abs(ctxMachine.GetxInput()) > 0)
        {
            // float increment = contextStateMachine.GetxInput() * contextStateMachine.acceleration;
            // float newSpeed = Mathf.Clamp(contextStateMachine.body.velocity.x + increment, -contextStateMachine.groundSpeed, contextStateMachine.groundSpeed);
            // contextStateMachine.body.velocity = new Vector2(newSpeed, contextStateMachine.body.velocity.y) ;
            float targetSpeed = ctxMachine.GetxInput() * ctxMachine.playerData.moveSpeed;
            float speedDif = targetSpeed - ctxMachine.body.velocity.x;
            float accelRate = (Mathf.Abs(speedDif) > 0.0f) ? ctxMachine.playerData.acceleration : ctxMachine.playerData.decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, ctxMachine.playerData.velPower) * Mathf.Sign(speedDif);
        
            ctxMachine.body.AddForce(movement * Vector2.right);
        }
        
    }
    private void CutAttack()
    {
        
    }
}
