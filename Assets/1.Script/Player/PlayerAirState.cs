using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
    {
    }

    public override void Enter()
    {
        base.Enter();


       // Debug.Log("��������");

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
        {

            if(player.isUpperPlayer)
            {
                if(player.downPlayer != null)
                {
                    if(player.downPlayer.currState is PlayerGroundedState == true)
                    stateMachine.ChangeState(player.State_Stair);

                }




            }                
                
  
            else if(player.isGround)
             stateMachine.ChangeState(player.State_idle);

        }


        if (player._colChecker.IsFrontObject())
        {
            player.SetVelocity(0, player.rb.velocity.y);
            //player.pv.RPC("SetPlayerVelocity", RpcTarget.All, 0, rb.velocity.y);
        }
        else
        {
            if (xInput != 0)
            {
                player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
             //   player.pv.RPC("SetPlayerVelocity", RpcTarget.All, player.moveSpeed * 0.8f * xInput, rb.velocity.y);


            }

        }


        if (player.isGimmicked)
            stateMachine.ChangeState(player.State_Hit);
        if (player.isDead)
        {
            stateMachine.ChangeState(player.State_Dead);
        }

    }
}
