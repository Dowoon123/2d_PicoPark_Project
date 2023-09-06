using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState  
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController player;


    protected Rigidbody2D rb;


    protected float xInput;
    protected float yInput;
    private string animBoolName;
    public STATE_INFO state_info;

    protected float stateTimer;
    protected bool triggerCalled;
    


    public PlayerState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
        this.state_info = _info;
    }


    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);


       // player._colChecker.obstacleObject �� ������Ʈ�� isCheckPush �� 
       // �ϴܿ��⼭ �������� �����ؿ� 
       // move���ǵ��� 0.6�������� �׸��� �� ���ν�Ƽ ���� ���Ŀ�
       // �ڽ��Ǻ��ν�Ƽ�� �����ν�Ƽ�� �����Ѵ� 

    }
  

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
   

}
