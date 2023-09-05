using UnityEngine;


public abstract class GhostState
{
    protected GhostController ghostController;
    protected GhostStateMachine stateMachine;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    protected float stateTimer;
    private string animBoolName;

    public gSTATE_INFO state_info;

    public GhostState(GhostController _ghostController, GhostStateMachine _stateMachine, string _animBoolName, STATE_INFO _info)
    {
        this.ghostController = _ghostController;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    protected GhostState(GhostController ghostController, GhostStateMachine stateMachine, string animBoolName)
    {
        this.ghostController = ghostController;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        triggerCalled = false;


    }

    public virtual void Exit()
    {

    }
}

