using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerHitState : PlayerState
{
    float hitTimer = 0;
   
    public PlayerHitState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
    {
       
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("히트 상태");
        player._colChecker.JumpCollider(false);
        rb.AddForce(new Vector2(-1 * 200f, 100f), ForceMode2D.Impulse);

        hitTimer = 0.3f;
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

        hitTimer -= Time.deltaTime;

        if( hitTimer < 0)
        {
            hitTimer = 0;
            HitOver();
        }
    }

    public void HitOver()
    {
       
        player.stateMachine.ChangeState(player.State_idle);
    }
}
