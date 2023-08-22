using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerState
{
    public PlayerPushState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(" Push 상태 진입");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
