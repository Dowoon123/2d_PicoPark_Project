using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostIdleState : GhostState
{
    public GhostIdleState(GhostController _ghostController, GhostStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_ghostController, _stateMachine, _animBoolName, _info)
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
    }
}
