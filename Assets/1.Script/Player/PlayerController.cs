using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public enum STATE_INFO
{
    IDLE,
    MOVE,
    JUMP,
    AIR,
    PUSH,
}

public class PlayerController : MonoBehaviourPunCallbacks
{
    public STATE_INFO currStateEnum = STATE_INFO.IDLE;
    // 플레이어의 정보를 담고 있다. 이 정보는
    // 닉네임, 닉네임 UI Text , 그리고 Player가 보유한 조작가능한 오브젝트를 가지고 있다. 

    #region collision
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] Transform GroundChecker;

    #endregion

    #region Components
    public Rigidbody2D rb;
    public PlayerState currState;
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
    public GameObject downPlayer;
    public GameObject m_stateCanvas;
    public Text stateTxt;


    // Start is called before the first frame update
    void Awake()
    {
       

        rb = GetComponent<Rigidbody2D>();
        _colChecker = GetComponent<collideChecker>();
        anim = GetComponentInChildren<Animator>();

        stateMachine = new PlayerStateMachine();
        stateMachine.player = this;

        State_idle = new PlayerIdleState(this, stateMachine, "Idle",STATE_INFO.IDLE);
        State_move = new PlayerMoveState(this, stateMachine, "Move",STATE_INFO.MOVE);
        State_Jump = new PlayerJumpState(this, stateMachine, "Jump", STATE_INFO.JUMP);
        State_Air = new PlayerAirState(this, stateMachine, "Idle",STATE_INFO.AIR);
        State_Push = new PlayerPushState    (this, stateMachine, "Push",STATE_INFO.PUSH);
       // State_AirPush = new PlayerAirPushState(this, stateMachine, "Idle");

      
        

    }


    [PunRPC]
    public void SetCurrState(STATE_INFO _State)
    {
        PlayerState st = null;
        switch(_State)
        {
            case STATE_INFO.IDLE:
                st = State_idle;
                break;
            case STATE_INFO.JUMP:
                st = State_move;
                break;
            case STATE_INFO.AIR:
                st = State_Air;
                break;
            case STATE_INFO.PUSH:
                st = State_Push;
                break;
            case STATE_INFO.MOVE:
                st = State_move;
                break;
        }

        currState = st;
        stateTxt.text = currState.ToString();

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

        if (!flip)
            _colChecker.playerChecker.transform.localPosition = new Vector2(-0.5f, 0f);
        else
            _colChecker.playerChecker.transform.localPosition = new Vector2(0.5f, 0f);

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
                downPlayer = box.gameObject;


            }
            else
            {
                isUpperPlayer = false;
                downPlayer = null;
                isGround = true;

            }

            return true;
        }
        else

        {

            isUpperPlayer = false;
            downPlayer = null;
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


    [PunRPC]
    public void SetVelocityNetwork(Vector3 velocity)
    {
        rb.velocity = velocity;
    }



}
