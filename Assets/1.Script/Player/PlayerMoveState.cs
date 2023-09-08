using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMoveState : PlayerGroundedState
{
    
     public collideChecker _colChecker;
    public PlayerMoveState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
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
        base.Update();

        // if(!player._colChecker.isObstacle)

        //  player.transform.Translate(xInput * Vector2.right * player.moveSpeed * Time.deltaTime);
        //   player.SetVelocity(, );




       //   player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

          

        
        if(!player.isReadyToClear)
        player.pv.RPC("SetPlayerVelocity", RpcTarget.All, xInput * player.moveSpeed, rb.velocity.y);

        if (player._colChecker.IsFrontObject())
        {
            GameObject targetObject = null;

            if (player.GetComponent<collideChecker>().PushedPlayer != null && player.GetComponent<collideChecker>().PushedPlayer.layer != 8)
                targetObject = player.GetComponent<collideChecker>().PushedPlayer;

            if (player.GetComponent<collideChecker>().obstacleObject != null && player.GetComponent<collideChecker>().obstacleObject.layer != 8)
                targetObject = player.GetComponent<collideChecker>().obstacleObject;


            if (targetObject != null)
            {

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
        }

        if (xInput == 0)
        {
            if(player.isGround)
            stateMachine.ChangeState(player.State_idle);
            else if ( player.isUpperPlayer)
            stateMachine.ChangeState(player.State_Stair);

        }


        if (player.isGimmicked)
            stateMachine.ChangeState(player.State_Hit);
    }
}
