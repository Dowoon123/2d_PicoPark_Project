using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;


public class MovedBlock : MonoBehaviourPunCallbacks
{
    [Header("수용된 인원")]
    /// <summary>
    /// 수용된 인원
    /// </summary>
    [SerializeField] private int CheckedPushingP = 0;
  
    [Header("수용 최대 인원(조건)")]
    /// <summary>
    /// 수용 하려는 최대인원 임의로 설정
    /// </summary>
    [SerializeField] private int CheckedPushAllPlayer;
 
    [SerializeField] private int facingDirCheck;
    [SerializeField] private List<GameObject> PushPlayer;
    [SerializeField] private Vector3 CheckRect;
    private Rigidbody2D rb;





    [SerializeField] protected LayerMask whatIsGround;

    [SerializeField] private bool isCheckPush = false;
    //수용인원 조건이 충족하는지 체크하는 bool값
    //TRUE면 엘레베이터 작동


    //x축 이동거리
    public float moveX = 0.0f;
    //y축 이동거리
    public float moveY = 0.0f;

    public float rectXSize = 0;
    //시간
    public float times = 0.0f;
    //정지시간
    public float weight = 0.0f;




    //1프레임당 x 이동 값
    public float perDX;
    //1프레임당 y 이동 값
    public float perDY;

    //초기 위치
    Vector3 defpos;
    //반전 여부
    bool isReverse = false;


    // Start is called before the first frame update
    void Start()
    {
        // OnDrawGizmos();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckPlayerZone();

        var checkDirection = CheckedPushAllPlayer - facingDirCheck;
        var checkDirection2 = CheckedPushAllPlayer - Mathf.Abs(facingDirCheck);

        if (!isCheckPush)
        {
            if (CheckedPushAllPlayer == CheckedPushingP && checkDirection == 0)
            {
                GetComponent<PhotonView>().RPC("CheckPush", RpcTarget.AllBuffered, true);

            }
            else if ((CheckedPushAllPlayer - CheckedPushingP) == 0 && checkDirection2 == 0)
            {
                GetComponent<PhotonView>().RPC("CheckPush", RpcTarget.AllBuffered, true);
            }

        }

        if (CheckedPushAllPlayer != CheckedPushingP)
            GetComponent<PhotonView>().RPC("CheckPush", RpcTarget.AllBuffered, false);



        if (isCheckPush && facingDirCheck == CheckedPushAllPlayer)
        {
            rb.velocity = new Vector2(moveX, 0);
            Debug.Log("왼쪽에서 밀리고 있음.");
        }

        if (isCheckPush && -(facingDirCheck) == CheckedPushAllPlayer)
        {
            rb.velocity = new Vector2(-moveX, 0);
            Debug.Log("오른쪽에서 밀리고 있음.");
        }



    }


    [PunRPC]
    public void CheckPush(bool b)
    {
        isCheckPush = b;

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - new Vector3(0, 0, 0), CheckRect);
        // Gizmos.DrawCube(transform.position + new Vector3(rectXSize, 0,0) , CheckRect);
    }

    public void CheckPlayerZone()
    {


        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, CheckRect, 0, whatIsGround);


        PushPlayer.Clear();//일단 전부 지움 어차피 업데이트에서 무한으로 굴러감
        int check = 0;//지역 변수로 CHECK를 지정
        int facingcheck = 0;//지역 변수로 facingcheck 지정
        for (int i = 0; i < colliders.Length; ++i)
        {
            var player = colliders[i].GetComponent<PlayerController>();

            if (player.currState == player.State_Push && player.facingDir > 0)
            {
                check++;
                Debug.Log("오른쪽에서 감지 되었소");

                if (player.facingDir < 0)
                {
                    facingcheck++;

                }
                else if (player.facingDir > 0)
                {
                    facingcheck--;
                }


            }
            else if (player.stateMachine.currentState == player.State_Push && player.facingDir < 0)
            {
                check++;
                Debug.Log("왼쪽에서 감지 되었소");


                if (player.facingDir > 0)
                {

                    facingcheck--;
                }
                else if (player.facingDir < 0)
                {
                    facingcheck++;
                }
            }


            PushPlayer.Add(player.gameObject);
            //리스트에 var로 조장된 player는 결국 colliders[i]. 리스트에 할당 받은 것이니 리스트에 저장

        }

        facingDirCheck = facingcheck;

        CheckedPushingP = check; //check를 0으로 조정 사실상 Clear()역할임.

        if (colliders.Length == 0)
        {
            CheckedPushingP = 0;
            facingDirCheck = 0;
        }

        //colliders의 리스트 값이 없을때. colliders.[0] 이 할당 한번이라도 받으면 1이니까.
        //CheckedIntake의 값을 0으로 초기화



        //Collider2D[] collidersR = Physics2D.OverlapBoxAll(transform.position, CheckRect, 0, whatIsGround);
    }
}
