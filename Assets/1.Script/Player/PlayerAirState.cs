using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


        Debug.Log("에어진입");

        player._colChecker.JumpCollider(false);
    }

    public override void Exit()
    {
        base.Exit();
        player._colChecker.JumpCollider(true);
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.State_idle);


        if (player._colChecker.IsPlayerDetected())
            player.SetVelocity(0, player.rb.velocity.y);
        else
        {
            if (xInput != 0)
            {
                player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);


            }

        }
    }
}
