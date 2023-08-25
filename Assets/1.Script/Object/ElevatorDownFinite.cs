using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDownFinite : MonoBehaviour
{
    /*
       * 이 스크립트는 유한이 이동하는 엘레베이터 스크립트임.
       * 단, Y축 위의 이동에 대한 범위만 사용
       * 아래는 귀찮아서 Down으로 쓸거임.
       * 
       * 
       */
    [Header("수용된 인원")]
    [SerializeField] private int CheckedIntake = 0; //수용된 인원
    [Header("수용 최대 인원(조건)")]
    [SerializeField] private int intake; //수용 하려는 인원 임의로 설정
    [SerializeField] private List<GameObject> UpSidePlayer;


    [SerializeField] private bool endX = false; //작동 여부를 체크할 부울 변수지만, 이번 스크립트에서는 사용하지 않을 것임.
    [SerializeField] private bool endY = false;
    [Header("플레이어를 감지하기 위한 범위")]
    [SerializeField] private Vector3 CheckRect;//설정한 범위 만큼 체크 하기 위한 벡터3 변수임.
    [Header("도착 지점 설정 벡터 변수 y값만 사용 아래로만감")]
    [SerializeField] private Vector3 arrivePos;//도착 위치를 임의 설정하기 위한 벡터3 변수

    [SerializeField] protected LayerMask whatIsGround;


    //x축 이동거리
    public float moveX = 0.0f;
    //y축 이동거리
    public float moveY = 0.0f;
    //시간
    public float times = 0.0f;
    //정지시간
    public float weight = 0.0f;

    [SerializeField] private bool isCheckIntake = false;
    //수용인원 조건이 충족하는지 체크하는 bool값
    //TRUE면 엘레베이터 작동


    //1프레임당 x 이동 값
    public float perDX;
    //1프레임당 y 이동 값
    public float perDY;

    //초기 위치
    Vector3 defpos;
    //반전 여부
    bool isReverse = false;




    void Start()
    {
        //초기위치
        defpos = transform.position;
        //1프레임에 이동하는 시간
        float timestep = Time.deltaTime;
        //1프레임 X 이동값
        perDX = moveX / (1.0f / timestep * times);
        //1프레임의 Y 이동 값
        perDY = moveY / (1.0f / timestep * times);


    }

    private void Update()
    {
        CheckPlayerZone();
    }

    private void FixedUpdate()
    {


        if ((intake - CheckedIntake) == 0)
        {
            isCheckIntake = true;

        }
        else
            isCheckIntake = false;//업데이트 문에서 이 조건이 항상 참인지 거짓인지 계속 체크할거임.

        if (isCheckIntake)
        {//정방향(아래)이동

            Debug.Log("내려가고이씅ㅁ");
            //블록 이동
            Vector3 v = new Vector3(-perDX, -perDY, defpos.z);
            transform.Translate(v);


            if (transform.position.y < arrivePos.y)
                transform.position = new Vector3(transform.position.x, arrivePos.y, transform.position.z);
        }
        else if (!isCheckIntake)
        {

            if (transform.position.y > defpos.y)
                transform.position = defpos;
            //내려가는 최소 높이는 = 기존에 있던 위치임.

            //블록 이동
            transform.Translate(new Vector3(perDX, perDY, defpos.z));
        }




    }


    public void CheckPlayerZone()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, CheckRect, 0, whatIsGround);

        UpSidePlayer.Clear();//일단 전부 지움 어차피 업데이트에서 무한으로 굴러감
        int check = 0;//지역 변수로 CHECK를 지정
        for (int i = 0; i < colliders.Length; ++i)
        {
            var player = colliders[i].GetComponent<PlayerController>();

            if (player.isGround || player.isUpperPlayer)
                check++;

            UpSidePlayer.Add(player.gameObject);
            //리스트에 var로 조장된 player는 결국 colliders[i]. 리스트에 할당 받은 것이니 리스트에 저장
        }

        CheckedIntake = check; //check를 0으로 조정 사실상 Clear()역할임.

        if (colliders.Length == 0)
            CheckedIntake = 0;
        //colliders의 리스트 값이 없을때. colliders.[0] 이 할당 한번이라도 받으면 1이니까.
        //CheckedIntake의 값을 0으로 초기화

    }

}
