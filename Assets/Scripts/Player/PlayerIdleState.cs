using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void Enter()
    {
        // Debug.Log("IDLE");
        contextStateMachine = gameObject.GetComponentInParent<PlayerStateMachine>();
    }
    public override void Do()
    {
        CheckSwitchState(this);
        BumpAttack();
        DefenseAttack();
    }


    public override void FixedDo()
    {

    }
    public override void Exit(PlayerBaseState newSubState)
    {
        contextStateMachine.subState = newSubState;
        newSubState.Enter();

    }
    public override void CheckSwitchState(PlayerBaseState actualSubState)
    {
        if(Mathf.Abs(contextStateMachine.GetxInput()) > 0)
        {
            PlayerBaseState newSubState = player.GetComponentInChildren<PlayerRunningState>();
            actualSubState.Exit(newSubState);
        }
    }
    private void BumpAttack()
    {
        if(contextStateMachine.superState == player.GetComponentInChildren<PlayerGroundState>() && contextStateMachine.GetAtkInput() > 0)
        {
            attackColliderObject.SetActive(true);
            contextStateMachine.SetAimDirection(0,1);
        }
    }

    private void DefenseAttack()
    {
        if(contextStateMachine.superState == player.GetComponentInChildren<PlayerAirState>() && contextStateMachine.GetAtkInput() > 0)
        {
            contextStateMachine.SetAimDirection(0.5f,1);
        }

    }

}
