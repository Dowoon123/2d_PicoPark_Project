using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerGroundedState
{
    public PlayerPushState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
        
        Debug.Log("푸쉬");
        Debug.Log(" Push 상태 진입");
    }

    public override void Exit()
    {
        base.Exit();

    }


    public override void Update()
    {
        base.Update();

        if (player._colChecker.obstacleObject)
        {



            var box = player._colChecker.obstacleObject.GetComponent<MovedBlock>();

            Debug.Log(player._colChecker.obstacleObject);

            if (!box.isCheckPush)
                player.SetVelocity(0, player.rb.velocity.y);
            else
            {
                player.SetVelocity(xInput * player.moveSpeed * 0.4f, 0);
                box.gameObject.GetComponent<Rigidbody2D>().velocity = player.rb.velocity;
            }
        }


        if (xInput == 0)

        {
            player.stateMachine.ChangeState(player.State_idle);
            return;
        }


        if (!player._colChecker.IsFrontObject())
         player.stateMachine.ChangeState(player.State_idle);
       
        
    }
}
