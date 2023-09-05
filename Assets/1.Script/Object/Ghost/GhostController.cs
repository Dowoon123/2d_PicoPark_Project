using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public enum gSTATE_INFO
{
    IDLE,
    TRACE
}

public class GhostController : MonoBehaviour
{

    public GameObject idleGhost;
    public GameObject TraceGhost;

    public Transform target; // 추적할 대상
    public float detectionRange = 5f; // 감지 범위

    private Vector2 currentVelocity; // 현재 속도
    private bool isBeingWatched = false; // 대상을 바라보고 있는지 여부

    #region Components
    public Rigidbody2D rb;
    public PlayerState currState;
    public GhostStateMachine stateMachine;
    #endregion

    #region  moveInfo
    public float moveSpeed = 5.5f;

    GameObject PlayerAbleCharacter;
    string nickname;
    GameObject NicknameText;
    #endregion

    #region states
    public GhostIdleState State_idle;
    public GhostTraceState State_trace;
    #endregion

    public int facingDir { get; set; } = 1;
    protected bool facingRight = false;

    int _actorNumber;
    public int actorNumber
    {
        get { return _actorNumber; }
        set { _actorNumber = value; }

    }

    public PhotonView pv;

    void Awake()
    {

        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
      //  _colChecker = GetComponent<collideChecker>();
      //  anim = GetComponentInChildren<Animator>();

        stateMachine = new GhostStateMachine();
        stateMachine.ghost = this;

        State_idle = new GhostIdleState(this, stateMachine, "Idle", STATE_INFO.IDLE);
        State_trace = new GhostTraceState(this, stateMachine, "Trace", STATE_INFO.MOVE);

        // State_AirPush = new PlayerAirPushState(this, stateMachine, "Idle");



        //if( PhotonNetwork.IsMasterClient)
        //{
        //     GetComponent<PhotonView>().RPC("testSetCube", RpcTarget.All);

        //}

    }

    private void Start()
    {
        TraceGhost.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        target = gameObject.transform.Find("Player");
    }
    private void Update()
    {
        // 대상과의 거리를 계산
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 대상이 감지 범위 내에 있을 때
        if (distanceToTarget <= detectionRange)
        {
            // 대상을 바라보는 방향 계산
            Vector3 targetDirection = (target.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, targetDirection);

            // 대상을 바라보고 있으면
            if (angleToTarget <= 30f)
            {
                isBeingWatched = true;
            }
            else
            {
                isBeingWatched = false;
            }

            // 대상을 바라보고 있지 않으면 이동
            if (!isBeingWatched)
            {
                Vector2 targetDir = new Vector2(targetDirection.x, targetDirection.y);
                currentVelocity = Vector2.MoveTowards(currentVelocity, targetDir * moveSpeed, Time.deltaTime);
                transform.Translate(currentVelocity * Time.deltaTime);
            }
            else
            {
                currentVelocity = Vector2.zero;
            }
        }
        else
        {
            isBeingWatched = false;
            currentVelocity = Vector2.zero;
        }
    }
}
