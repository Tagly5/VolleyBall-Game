using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public override void Enter()
    {
        // contextStateMachine = this.GetComponentInParent<PlayerStateMachine>();
        Debug.Log("Ground entry");
    }
    public override void Do()
    {
        CheckSwitchState(this);
    }
    public override void CheckSwitchState(PlayerBaseState actualState)
    {
        if (!contextStateMachine.grounded)
        {

        }
    }
    public override void Exit(PlayerBaseState newState)
    {
        contextStateMachine.superState = newState;
        newState.Enter();
        StopAllCoroutines();
    }
}
