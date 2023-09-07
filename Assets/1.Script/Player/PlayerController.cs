using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public enum STATE_INFO
{
    IDLE,
    MOVE,
    JUMP,
    AIR,
    PUSH,
    HIT,
    DEAD,
    STAIR,

}

public class PlayerController : MonoBehaviourPunCallbacks
{
    public STATE_INFO currStateEnum = STATE_INFO.IDLE;


    #region ROPE
    public bool isRope;
    public GameObject conectedBody;

    #endregion


    #region network
    private Vector2 networkPostion;
    private Vector2 networkVelocity;
    private float serverTime;
    public PhotonView pv;
    public int prevId;
    public bool isTransfer;
    public Vector2[] offsetArray = new Vector2[4];
    public PlayerController[] upsideArray = new PlayerController[4];
    public int arrayLength = 0;
    public int offsetIndex = 10;
    public bool isLeftEdge = false;
    public bool isRightEdge = false;
    public string NickName;
    #endregion


    #region collision
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] Transform GroundChecker;
    public bool isNearDoor = false;
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
    public PlayerHitState State_Hit;
    public PlayerDeadState State_Dead;
    public PlayerStairState State_Stair;
    public Player_GunIdle State_GunIdle;
    public Player_GunMove State_GunMove;
    public Player_GunJump State_GunJump;



    #endregion


    public int facingDir { get; set; } = 1;
    protected bool facingRight = false;

    int _actorNumber;
    public int actorNumber
    {
        get { return _actorNumber; }
        set { _actorNumber = value; }

    }

    public bool isDead = false;
    public bool isGround = false;
    public bool isUpperPlayer = false;
    public bool isGimmicked = false; //
    public bool isGunMode = false;
    public PlayerController downPlayer;
    public GameObject m_stateCanvas;
    public Text stateTxt;
    public GameObject nextstage;

    // Start is called before the first frame update
    void Awake()
    {

        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        _colChecker = GetComponent<collideChecker>();
        anim = GetComponentInChildren<Animator>();

        stateMachine = new PlayerStateMachine();
        stateMachine.player = this;

        State_idle = new PlayerIdleState(this, stateMachine, "Idle", STATE_INFO.IDLE);
        State_move = new PlayerMoveState(this, stateMachine, "Move", STATE_INFO.MOVE);
        State_Jump = new PlayerJumpState(this, stateMachine, "Jump", STATE_INFO.JUMP);
        State_Air = new PlayerAirState(this, stateMachine, "Idle", STATE_INFO.AIR);
        State_Push = new PlayerPushState(this, stateMachine, "Push", STATE_INFO.PUSH);
        State_Hit = new PlayerHitState(this, stateMachine, "Hit", STATE_INFO.HIT);
        State_Dead = new PlayerDeadState(this, stateMachine, "Dead", STATE_INFO.DEAD);
        State_Stair = new PlayerStairState(this, stateMachine, "Idle", STATE_INFO.STAIR);
        
        // State_AirPush = new PlayerAirPushState(this, stateMachine, "Idle");


      



    }


    [PunRPC]
    public void SetCurrState(STATE_INFO _State)
    {
        PlayerState st = null;
        switch (_State)
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
            case STATE_INFO.HIT:
                st = State_Hit;
                break;
            case STATE_INFO.STAIR:
                st = State_Stair;
                break;
            case STATE_INFO.DEAD:
                st = State_Hit;
                break;
        }

        currState = st;
       // stateTxt.text = currState.ToString();

    }
    private void Start()
    {
        stateMachine.Initialize(State_idle);

        if (pv.IsMine)
        {
            string str = PhotonNetwork.LocalPlayer.NickName;
            pv.RPC("SetNickName", RpcTarget.AllBuffered,str);
        }

    }
    // Update is called once per frame
    void Update()
    {


        // 이부분 Groun
      


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
                     downPlayer = box.gameObject.GetComponentInParent<PlayerController>();


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
    public void SetTransform(Vector2 target)
    {
        transform.position = target;

  


    }

 
    [PunRPC]
    public void SetNickName(string str)
    {
        stateTxt.text = str;
    }


    [PunRPC]
    public void SetOffset(int viewID , Vector2 offset)
    {
       // 먼저 상대에게 offSetList를 줘야하고 그 offSetList를 비교해서
       // 만약거기 내원래 좌표가있었다면 그걸 없애고 내껄넣는다. 


        var player = PhotonView.Find(viewID);
        var pcl = player.GetComponent<PlayerController>();

        if (offsetIndex == 10)
        {
            for (int i = 0; i < pcl.offsetArray.Length; ++i)
            {

                if (pcl.offsetArray[i] == Vector2.zero)
                {
                    pcl.offsetArray[i] = offset;
                    offsetIndex = i;
                    pcl.upsideArray[i] = this;

                    Debug.Log("오프셋 :" + i + " 번째에 " + pcl.offsetArray[i] + " 적용");
                    pcl.arrayLength += 1;
                    return;
                }
            }
        }
        else
        {
            pcl.offsetArray[offsetIndex] = offset;
            pcl.arrayLength += 1;
            pcl.upsideArray[offsetIndex] = this;
            Debug.Log("오프셋 :" + offsetIndex + " 번째에 " + pcl.offsetArray[offsetIndex] + " 적용 , 이미 있을때");

        }

    }
    [PunRPC]
    public void DeSetOffset(int viewID)
    {


        var player = PhotonView.Find(viewID);
        var pcl = player.GetComponent<PlayerController>();

        pcl.offsetArray[offsetIndex] = Vector2.zero;
        pcl.upsideArray[offsetIndex] = null;
        offsetIndex = 10;
        pcl.arrayLength -= 1;

    }
    [PunRPC]
    public void ChangeOtherVel(int viewID)
    {
        var pc = PhotonView.Find(viewID).GetComponent<PlayerController>();

        if(pc)
        {
            pc.SetVelocity(pc.rb.velocity.x, 0);

        }
    }


    [PunRPC]
    void SetPlayerVelocity(float xVelocity, float yVelocity)
    {

        SetVelocity(xVelocity, yVelocity);


        if (arrayLength > 0)
        {
            for (int i = 0; i < upsideArray.Length; i++)
            {
                if (upsideArray[i] != null)
                {
                    Vector2 targetPos = transform.position;
                    targetPos += offsetArray[upsideArray[i].offsetIndex];
                    upsideArray[i].transform.position = targetPos;
                }

            }

        }

        if(conectedBody != null)
        {
            if(!conectedBody.GetComponent<PlayerController>().IsGroundDetected() )
            {
                if (conectedBody.GetComponent<PlayerController>().currState is PlayerAirState)
                    return;

                var dist = Vector2.Distance(transform.position, conectedBody.transform.position);
                if( dist > 8.0f)
                conectedBody.GetComponent<PlayerController>().SetVelocity(conectedBody.GetComponent<PlayerController>().rb.velocity.x, 5.5f);
            }
        }

      
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {

        // State 객체는 컬라이더를 가지고 있지않아서 OnTriggerEnter2D가 작동하지않습니다.
        // 제가보기엔 이 충돌체크는 문에서 처리해야할거같아요 : Class Door

        // 문에서 충돌체크를하면 collision.gameObject.Getcomponent<PlayerController>().nextStage = this.gameobject; 
        Debug.Log("콜리젼체크시도 ");
        if (collision.gameObject.CompareTag("Door"))
        {
            nextstage = collision.gameObject;
            Debug.Log("콜리젼체크 " + nextstage);
        }


     
    }

}




// 플레이어가 공에 닿으면 튕겨지는 부분 구현중

//private void OnCollisionEnter2D(Collision2D collision)
//{
//    float[] arrAngles = { -75, -60, -45, -30, -15, 0, 15, 30, 45, 60, 75 };

//    if (collision.collider.CompareTag("Ball"))
//    {
//        int r = Random.Range(0,arrAngles.Length);
//        Vector3 tmp = collision.transform.eulerAngles;
//        tmp.z = arrAngles[r];
//        collision.transform.eulerAngles = tmp;
//    }
//}


