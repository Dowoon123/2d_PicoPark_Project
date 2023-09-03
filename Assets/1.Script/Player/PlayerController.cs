using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    #endregion


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
    public PlayerHitState State_Hit;
    public PlayerDeadState State_Dead;
    public PlayerStairState State_Stair;



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
    public bool isGimmicked = false; //8.30 ï¿½ï¿½ï¿?ï¿½ï¿½ï¿½ï¿½ ï¿½Ûµï¿½ ï¿½ï¿½ï¿½Î¸ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ß°ï¿½ï¿½Ï¿ï¿½ï¿½ï¿½
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



        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NextStage();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ComeBackStage();
        }


        if (GetComponent<PhotonView>().IsMine)

        {

            stateMachine.currentState.Update();

        }




    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Door"))
        {
            nextstage = collision.gameObject;
        }
    }
    public void NextStage()
    {
        if (!nextstage)
        {
            Debug.Log("´ë±â");
        }
        else if (nextstage)
        {
            Debug.Log("½ºÅ×ÀÌÁöÅ¬¸®¾î");
            Time.timeScale = 0;
            transform.localScale = new Vector3(0, 0, 0);

        }
    }
         public void ComeBackStage()
    {
        if (!nextstage)
        {
            Debug.Log("´ë±â");
        }
        else if (nextstage)
        {
            Debug.Log("½ºÅ×ÀÌÁöº¹±Í");
            Time.timeScale = 1;
            transform.localScale = new Vector3(1, 1, 1);
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

        [PunRPC]
        void SetPlayerVelocity(float xVelocity, float yVelocity)
        {
            // ¿òÁ÷ÀÓ Ã³¸® ·ÎÁ÷
            SetVelocity(xVelocity, yVelocity);
        }



    }

    [PunRPC]
    public void SetOffset(int viewID , Vector2 offset)
    {
       // ¸ÕÀú »ó´ë¿¡°Ô offSetList¸¦ Áà¾ßÇÏ°í ±× offSetList¸¦ ºñ±³ÇØ¼­
       // ¸¸¾à°Å±â ³»¿ø·¡ ÁÂÇ¥°¡ÀÖ¾ú´Ù¸é ±×°É ¾ø¾Ö°í ³»²¬³Ö´Â´Ù. 


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

                    Debug.Log("¿ÀÇÁ¼Â :" + i + " ¹øÂ°¿¡ " + pcl.offsetArray[i] + " Àû¿ë");
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
            Debug.Log("¿ÀÇÁ¼Â :" + offsetIndex + " ¹øÂ°¿¡ " + pcl.offsetArray[offsetIndex] + " Àû¿ë , ÀÌ¹Ì ÀÖÀ»¶§");

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

    }



    [PunRPC]
    void SendOwner(int viewId)
    {

        pv.TransferOwnership(viewId);
        isTransfer = true;


    }

    }




// ÇÃ·¹ÀÌ¾î°¡ °ø¿¡ ´êÀ¸¸é Æ¨°ÜÁö´Â ºÎºÐ ±¸ÇöÁß

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


