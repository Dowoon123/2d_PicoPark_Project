using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Unity.IO.LowLevel.Unsafe;

public class PlayerController : MonoBehaviour
{

    // 플레이어의 정보를 담고 있다. 이 정보는
    // 닉네임, 닉네임 UI Text , 그리고 Player가 보유한 조작가능한 오브젝트를 가지고 있다. 


    #region collision
    [SerializeField] float groundCheckDist = 1f;
    [SerializeField]LayerMask WhatIsGround;
    #endregion

    #region Components
    public Rigidbody2D rb;
    public collideChecker _colChecker;
   public Animator anim;
    public PlayerStateMachine stateMachine;
    #endregion



    #region  moveInfo
    public float jumpForce = 8.5f;
    public float moveSpeed = 5.5f;

    GameObject PlayerAbleCharacter;
    string nickname;
    GameObject NicknameText;
    #endregion


    #region states
    public  PlayerIdleState State_idle;
    public  PlayerMoveState State_move;
    public PlayerJumpState State_Jump;
    public PlayerAirState State_Air;
    #endregion

    int _actorNumber;
    public int actorNumber
    {
        get { return _actorNumber; }
        set { _actorNumber = value; }

    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _colChecker = GetComponent<collideChecker>();
        anim = GetComponentInChildren<Animator>();

        stateMachine = new PlayerStateMachine();

        State_idle = new PlayerIdleState(this, stateMachine, "Idle");
        State_move = new PlayerMoveState(this, stateMachine, "Move");
        State_Jump = new PlayerJumpState(this, stateMachine, "Jump");
        State_Air = new PlayerAirState(this, stateMachine, "Idle");
    }

    private void Start()
    {
        stateMachine.Initialize(State_idle);
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            stateMachine.currentState.Update();

           

    

        }

     
  
    }


    public void CheckInput()
    {

    }
    public void SetPlayerCharacter(GameObject obj)
    {
        PlayerAbleCharacter = obj;
    }
    public GameObject GetPlayerCharacter()
    {
        return PlayerAbleCharacter;
    }


    public void SetPlayerNickName(string nick)
    {
        nickname = nick;
    }
    public string GetPlayerNickName()
    {
        return nickname;
    }

    public void SetPlayerNickNameText(GameObject nick)
    {
        NicknameText = nick;
    }
    public GameObject GetPlayerNickNameText()
    {
        return NicknameText;
    }


    public bool IsGroundDetected() => Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, WhatIsGround);

    public void ZeroVelocity() => rb.velocity = Vector2.zero;

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
      //  FlipController(_xVelocity);
    }

}
