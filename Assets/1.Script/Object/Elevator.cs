using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    /*
     * 이 스크립트는 무한히 올라가는 엘리베이터 기능이 탑재된
     * 스크립트임 X축 이동을 원하면 MoveX 값을
     * Y축 이동을 원하면 MoveY 값을 입력하면됨.
     * 단) 무한히 위로만 올라가는 스크립트이며, 조건을 충족하지 않을시 제자리로 돌아감.
     */
   

    [SerializeField] private int CheckedIntake = 0; //수용된 현재 인원

    [SerializeField] private int maxIntake; //수용 하려는 최대 인원 임의로 설정
    [SerializeField] private List<GameObject> UpSidePlayer;


    [SerializeField] private bool endX = false;
    [SerializeField] private bool endY = false;
    [SerializeField] private Vector3 CheckRect;


    [SerializeField] protected LayerMask whatIsGround;
    //말만 WhatIsGround지 Player체크 해야됨


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


        if ((maxIntake - CheckedIntake) == 0)
        {
            isCheckIntake = true;

        }
        else
            isCheckIntake = false;//업데이트 문에서 이 조건이 항상 참인지 거짓인지 계속 체크할거임.
        




        //이동중
        float x = transform.position.x;
        float y = transform.position.y;


        if (isCheckIntake)
        {
            //블록 이동
            Vector3 v = new Vector3(perDX, perDY, defpos.z);
            transform.Translate(v);
        }
        else if (!isCheckIntake)
        {
           
            if (defpos.y > transform.position.y)
                transform.position = defpos;
            //내려가는 최소 높이는 = 기존에 있던 위치임.
        
            //블록 이동
            transform.Translate(new Vector3(-perDX, -perDY, defpos.z));
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

            //  PlayerGroundedState state = gameObject.GetComponent<PlayerGroundedState>();

            //if (player.isGround || player.isUpperPlayer)
            // if (player.currState == player.groundState)
            //if (player.currState.GetType() == typeof(PlayerGroundedState))
            if (player.currState is PlayerGroundedState)
                check++;
                
            

            Debug.Log("박스 진입 상태 : " + player.currState.GetType());

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
