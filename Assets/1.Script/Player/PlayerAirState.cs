using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAirState : PlayerGroundedState
{
    public PlayerAirState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();




        player._colChecker.JumpCollider(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.State_idle);


        if (player._colChecker.IsPlayerDetected())
        {
            GameObject targetObject = null;

            if (player.GetComponent<collideChecker>().playerObject != null)
                targetObject = player.GetComponent<collideChecker>().playerObject;


            if (targetObject.transform.position.x > player.transform.position.x)
            {
                if (xInput > 0)
                {
                    stateMachine.ChangeState(player.State_Push);
                }
            }
            else if (targetObject.transform.position.x < player.transform.position.x)
            {
                if (xInput < 0)
                {
                    stateMachine.ChangeState(player.State_Push);
                }
            }
        }

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
            
    }
}
