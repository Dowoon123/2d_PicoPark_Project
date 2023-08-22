using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMoveState : PlayerGroundedState
{
    
     public collideChecker _colChecker;
    public PlayerMoveState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (player._colChecker.IsPlayerDetected())
        {
            GameObject targetObject = null;

            if (player.GetComponent<collideChecker>().playerObject != null)
                targetObject = player.GetComponent<collideChecker>().playerObject;


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
        
        if (xInput == 0)
            stateMachine.ChangeState(player.State_idle);
    }
}
