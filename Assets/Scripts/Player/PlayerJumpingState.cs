using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    public bool jumpPerformed;
    public override void Enter()
    {
        jumpPerformed = false;
        // contextStateMachine = this.GetComponentInParent<PlayerStateMachine>();
        Debug.Log("Jumping entry");
    }
    public override void Do()
    {
        
        CheckSwitchState(this);
    }
    public override void FixedDo()
    {
        Jump();
    }
    private void Jump()
    {
        Rigidbody2D body = contextStateMachine.body;
        float jumpForce = contextStateMachine.GetJumpForce();

        if (!jumpPerformed)
        {
            // body.AddForce(new Vector2(body.velocity.x, contextStateMachine.GetyInput() * jumpForce), ForceMode2D.Impulse);
            body.velocity = new Vector2(body.velocity.x, contextStateMachine.GetyInput() * jumpForce);
            jumpPerformed = true;
        }
    }

    public override void CheckSwitchState(PlayerBaseState actualState)
    {
        if (contextStateMachine.body.velocity.y < 0f)
        {
            PlayerBaseState newState = contextStateMachine.gameObject.GetComponentInChildren<PlayerFallState>();
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
