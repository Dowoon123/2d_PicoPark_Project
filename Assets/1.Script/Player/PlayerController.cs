using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // �÷��̾��� ������ ��� �ִ�. �� ������
    // �г���, �г��� UI Text , �׸��� Player�� ������ ���۰����� ������Ʈ�� ������ �ִ�. 

    public Map1 stageManeger;

    #region collision
    [SerializeField] float groundCheckDist = 1f;
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] Transform GroundChecker;

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
    public PlayerIdleState State_idle;
    public PlayerMoveState State_move;
    public PlayerJumpState State_Jump;
    public PlayerAirState State_Air;
    public PlayerPushState State_Push;
    public PlayerAirPushState State_AirPush;
    #endregion

    public int facingDir { get; set; } = 1;
    protected bool facingRight = false;

    int _actorNumber;
    public int actorNumber
    {
        get { return _actorNumber; }
        set { _actorNumber = value; }

    }

    public bool isGround = false;
    public bool isUpperPlayer = false;

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
        State_Push = new PlayerPushState(this, stateMachine, "Push");
        State_AirPush = new PlayerAirPushState(this, stateMachine, "Idle");

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

    [PunRPC]
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;


        var flip = GetComponentInChildren<SpriteRenderer>().flipX == true ? false : true;
        GetComponentInChildren<SpriteRenderer>().flipX = flip;

        if(!flip)
        _colChecker.playerChecker.transform.localPosition = new Vector2(-0.5f,-0.05f);
        else
            _colChecker.playerChecker.transform.localPosition = new Vector2(0.5f, -0.05f);

        Debug.Log(facingDir + " " + facingRight);
    }

  

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            GetComponent<PhotonView>().RPC("Flip", RpcTarget.All);
           

        }
        else if (_x < 0 && facingRight)
        {
            GetComponent<PhotonView>().RPC("Flip", RpcTarget.All);

        }
    }

    public bool IsGroundDetected()
    {
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, WhatIsGround);
        

        var box = Physics2D.OverlapBox(GroundChecker.position, new Vector2(0.49f, 0.15f), 0, WhatIsGround);
        if (box)
        {
            if (box.gameObject.layer == 7)
            {
                isGround = false;
                isUpperPlayer = true;

                _colChecker.JumpCollider(false);

            }
            else
            {
                isUpperPlayer = false;
                isGround = true;
                _colChecker.JumpCollider(true);
            }

            return true;
        }
        else

        {
            _colChecker.JumpCollider(true);
            isUpperPlayer = false;
            isGround = false;
            return false;

        }

    }


    public void ZeroVelocity() => rb.velocity = Vector2.zero;

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }



}
