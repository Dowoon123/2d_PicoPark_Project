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

    public Transform target; // ������ ���
    public float detectionRange = 5f; // ���� ����

    private Vector2 currentVelocity; // ���� �ӵ�
    private bool isBeingWatched = false; // ����� �ٶ󺸰� �ִ��� ����

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
        // ������ �Ÿ��� ���
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // ����� ���� ���� ���� ���� ��
        if (distanceToTarget <= detectionRange)
        {
            // ����� �ٶ󺸴� ���� ���
            Vector3 targetDirection = (target.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, targetDirection);

            // ����� �ٶ󺸰� ������
            if (angleToTarget <= 30f)
            {
                isBeingWatched = true;
            }
            else
            {
                isBeingWatched = false;
            }

            // ����� �ٶ󺸰� ���� ������ �̵�
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
