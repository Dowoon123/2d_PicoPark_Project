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
        // Invoke("HitOver", 1f);
        //player.isGimmicked = false;
        rb.AddForce(new Vector2(-1 * 10f * Mathf.Abs(player.facingDir), 5f), ForceMode2D.Impulse);

        hitTimer = 1;
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
        /*
        if(!player.isGimmicked) 
        {
            player.stateMachine.ChangeState(player.State_idle);
        }*/
    }

    public void HitOver()
    {
       
        player.stateMachine.ChangeState(player.State_idle);
    }
}
