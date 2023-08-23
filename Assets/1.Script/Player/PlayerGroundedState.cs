using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerGroundedState : PlayerState
{


    PlayerController player;
    public PlayerGroundedState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
        player = _player;
       
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log(" �׶��� ���� ����");
        player.SetVelocity(player.rb.velocity.x, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if(player.isGround)
        {
            player._colChecker.JumpCollider(true);
        }
        else if (player.isUpperPlayer)
        {
            player._colChecker.JumpCollider(false);
        }

        //  if (!player.IsGroundDetected())
        //    stateMachine.ChangeState(player.airState);
        

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.State_Jump);
        }
    }
   
}
