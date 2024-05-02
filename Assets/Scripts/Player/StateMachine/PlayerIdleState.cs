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
        ctxMachine = gameObject.GetComponentInParent<PlayerStateMachine>();
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
        ctxMachine.subState = newSubState;
        newSubState.Enter();

    }
    public override void CheckSwitchState(PlayerBaseState actualSubState)
    {
        if(Mathf.Abs(ctxMachine.GetxInput()) > 0)
        {
            PlayerBaseState newSubState = player.GetComponentInChildren<PlayerRunningState>();
            actualSubState.Exit(newSubState);
        }
    }
    private void BumpAttack()
    {
        
    }

    private void DefenseAttack()
    {
        

    }

}
