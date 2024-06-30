using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public override void Enter()
    {
        // contextStateMachine = this.GetComponentInParent<PlayerStateMachine>();
        Debug.Log("Falling entry");
    }
    public override void Do()
    {
        CheckSwitchState(this);
    }
    public override void FixedDo()
    {

    }

    public override void CheckSwitchState(PlayerBaseState actualState)
    {
        if (contextStateMachine.grounded && contextStateMachine.body.velocity.y == 0f)
        {
            PlayerBaseState newState = contextStateMachine.gameObject.GetComponentInChildren<PlayerGroundState>();
            Exit(newState);
        }
    }
    public override void Exit(PlayerBaseState newState)
    {
        contextStateMachine.superState = newState;
        newState.Enter();
        StopAllCoroutines();
    }
}
