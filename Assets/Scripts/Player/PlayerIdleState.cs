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

}
