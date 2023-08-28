using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirPushState : PlayerState
{
    public PlayerAirPushState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.State_idle);
    

        if (player._colChecker.IsPlayerDetected())
        {
            player.SetVelocity(0, player.rb.velocity.y);

        }
        else
        {
            if(xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
            else if (xInput == 0)
                player.stateMachine.ChangeState(player.State_Air);

        }




    }



}
