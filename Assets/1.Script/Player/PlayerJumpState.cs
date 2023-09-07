using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    PlayerController player;


    public PlayerJumpState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
    {
     player = _player;
    }

    public override void Enter()
    {
        base.Enter();

        player._colChecker.JumpCollider(true);

        if (player.upsideArray[0] == null)
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        else if (player.upsideArray[0] != null)
          stateMachine.ChangeState(player.State_idle);

        //player._colChecker.JumpCollider(true);
        //Debug.Log("점프진입");

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

        if(player._colChecker.IsUpper())
        {
            int viewID = 0;
            player.pv.RPC("ChangeOtherVel", RpcTarget.AllBuffered, viewID);
        }

        


        if (player._colChecker.IsFrontObject())
        {
            player.SetVelocity(0, player.rb.velocity.y);
            //player.pv.RPC("SetPlayerVelocity", RpcTarget.All, 0, player.rb.velocity.y);
        }
        else
        {
            if (xInput != 0)
            {
                player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
               // player.pv.RPC("SetPlayerVelocity", RpcTarget.All, player.moveSpeed * 0.8f * xInput , player.rb.velocity.y);
            }
        }

        if (player.isGimmicked)
            stateMachine.ChangeState(player.State_Hit);

        if(player.isDead) 
        {
            stateMachine.ChangeState(player.State_Dead);
        }

   

    }
}
