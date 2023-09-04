using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerGroundedState : PlayerState
{


    PlayerController player;
    public GameObject nextstage;
    private List<Vector3> upsidePlayerTargetPositions = new List<Vector3>();
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


        if (Input.GetKeyDown(KeyCode.Space) && (player.isUpperPlayer || player.isGround))
        {
            
            stateMachine.ChangeState(player.State_Jump);
        }
       




        if (player._colChecker.UpsidePlayers.Count > 0 && xInput != 0)
        {
            for (int i = 0; i < player._colChecker.UpsidePlayers.Count; i++)
            {
                int view = player._colChecker.UpsidePlayers[i].pv.ViewID;
                player.pv.RPC("SetNetPos", RpcTarget.All,
                  view, player._colChecker.upPosition[i]);

            }
        }


        if (player.isGimmicked)
            stateMachine.ChangeState(player.State_Hit);\
    }
    
   
}



