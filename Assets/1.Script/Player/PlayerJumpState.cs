using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    PlayerController player;
    public PlayerJumpState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
     player = _player;
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);

        //player._colChecker.JumpCollider(true);

           
    }

    public override void Exit()
    {
        base.Exit(); 
    }

    public override void Update()
    {
        base.Update();

        if (player.rb.velocity.y < 0)
            stateMachine.ChangeState(player.State_Air);


        if (player._colChecker.IsPlayerDetected())
            player.SetVelocity(0, player.rb.velocity.y);
        else
        {


            if (xInput != 0)
                player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
    }
}
