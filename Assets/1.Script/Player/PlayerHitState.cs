using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
    {

    }

    public override void Enter()
    {
       base.Enter();
       Debug.Log("히트 상태");
       
       player.SetVelocity(-20 * Mathf.Abs(player.facingDir), 100);
       player._colChecker.JumpCollider(false);
        player.isGimmicked = false;
    }

    public override void Exit()
    {
        base.Exit();
        player._colChecker.JumpCollider(true);
    }

    public override void Update()
    {
        base.Update();
        if(!player.isGimmicked) 
        {
            stateMachine.ChangeState(player.State_idle);
        }
    }
}
