using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerGroundedState : PlayerState
{


    PlayerController player;

    public PlayerGroundedState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
    {
        player = _player;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log(" 그라운드 상태 진입");
        player.SetVelocity(player.rb.velocity.x, 0);
  
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.IsGroundDetected();



        if (player.isGround)
        {
            player._colChecker.JumpCollider(true);
        }
        else if (player.isUpperPlayer)
        {
            player._colChecker.JumpCollider(false);
       
        }

        //  if (!player.IsGroundDetected())
        //    stateMachine.ChangeState(player.airState);
        

        if (Input.GetKeyDown(KeyCode.Space) && (player.isUpperPlayer || player.isGround) )
        {
      

            stateMachine.ChangeState(player.State_Jump);
        }

        if(player._colChecker.UpsidePlayers.Count > 0)
        {
           
            for(int i=0; i< player._colChecker.UpsidePlayers.Count; i++)
            {
                if (player._colChecker.UpsidePlayers[i].currState == player._colChecker.UpsidePlayers[i].State_idle) ;
                player._colChecker.UpsidePlayers[i].GetComponent<PhotonView>().RPC("SetVelocityNetwork", RpcTarget.All, player.rb.velocity);
    
            }
           

        }

    }




}
