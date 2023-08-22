using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    PlayerController player;
    public PlayerIdleState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        player = _player;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log(" Idle 상태 진입");
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
