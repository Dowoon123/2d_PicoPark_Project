using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerPushState : PlayerGroundedState
{
    
    public PlayerPushState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
        Debug.Log("Çª½¬");
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, player.rb.velocity.y);


        if (xInput == 0)

        {
            player.stateMachine.ChangeState(player.State_idle);
            return;
        }


        if (!player._colChecker.IsPlayerDetected())
         player.stateMachine.ChangeState(player.State_idle);
       
        
    }
}
