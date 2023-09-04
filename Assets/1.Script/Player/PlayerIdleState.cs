using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    PlayerController player;
    public PlayerIdleState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
    {
        player = _player;
    }

    public override void Enter()
    {
        base.Enter();

       // Debug.Log(" Idle 상태 진입");
        player._colChecker.JumpCollider(false);

        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }



    public override void Update()
    {
        base.Update();


        if (xInput != 0)
            player.stateMachine.ChangeState(player.State_move);
    }


}
