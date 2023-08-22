using UnityEngine;

public class Elevator : MonoBehaviour
{
    /// <summary>
    /// 만들려는거, 인원을 체크하고, 그 인원이 점프상태가 아니고 범위 안에만 있고,
    /// 그 수 조건이 충족되면 엘리베이터 작동
    /// 그게 아닐경우 하강, 생각보다 쉬우면서도 복잡함.
    /// 
    /// 여기서 구현해야되는거, 돌아가는 조건의 로직 수정이 필요함.
    /// 플레이어가 지면 착지나, 플레이어의 위가 아닐때만 무조건 reverse되야함.
    /// 
    /// 지금은 작동 조건만 채워지면 무조건 위 아래로 이동함(자동 이동이니까)
    /// 
    /// 고쳐야 될것. 
    /// 
    /// 1. 위에 도착하면 기다린 후에 작동이 안되야함, 무조건 일시 정지.
    /// 
    /// 2. 그 자리에서 벗어날 경우 리버스.
    /// 
    /// 이게 먼저 구현되야함.
    /// 
    /// 
    /// bool isCheckIntake  체크만 했지 사용은 안했음.
    /// 벗어날 경우 반드시 강제로 리버스 시켜야됨.
    /// 
    /// 현재 올라가고 내려가는건 구현되었지만,
    /// Stop상태가 없음.
    /// 그걸 또 넣어야됨.
    /// 변수값 새로 넣어서 위치 강제로 고정 시킬까 생각중.
    /// 
    /// </summary>

    [SerializeField] private int CheckedIntake = 0; //수용된 인원

    [SerializeField] private int intake; //수용 하려는 인원 임의로 설정

    //x축 이동거리
    public float moveX = 0.0f;
    //y축 이동거리
    public float moveY = 0.0f;
    //시간
    public float times = 0.0f;
    //정지시간
    public float weight = 0.0f;

    //올라갔을때 움직이기 버릴거임.
    public bool isMoveWhenOn = false; //조건에 맞았을떄, 

    [SerializeField] private bool isCheckIntake = false;
    //수용인원 조건이 충족하는지 체크하는 bool값

    //움직임을 승인하는 불값 체크
    public bool isCanMove = true;

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


    private void FixedUpdate()
    {

        Debug.Log("Player 체크 : " + CheckedIntake);

        if ((intake - CheckedIntake) == 0)
        {
            isCheckIntake = true;
            
        }
        else
            isCheckIntake = false;//업데이트 문에서 이 조건이 항상 참인지 거짓인지 계속 체크할거임.

        Debug.Log("isCheckIntake 체크 : " + isCheckIntake);

        //여기까진 문제없음.

        if (isCanMove)
        {
            //이동중
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;

            if (isCheckIntake)
            {//정방향이동
                //이동량이 양수고 이동위치가 초기 위치보다 크거나
                //이동량이 음수고 이동 위치가 초기 위치보다 작은경우
                if ((perDX >= 0.0f && x >= defpos.x + moveX) || (perDX < 0.0f && x < defpos.x + moveX))
                {
                    endX = true; //X방향 이동 종료
                }
                if ((perDY >= 0.0f && y >= defpos.y + moveY) || (perDY < 0.0f && y < defpos.y + moveY))//x로 되있길래 y로 바꿧음
                {
                    endY = true; //Y방향 이동 종료
                }
                //블록 이동
                Vector3 v = new Vector3(perDX, perDY, defpos.z);
                transform.Translate(v);
            }
            else if (!isCheckIntake)
            {
                if ((perDX >= 0.0f && x <= defpos.x) || (perDX <= 0.0f && x >= defpos.x))
                {
                    //이동량이 +
                    endX = true;//X방향 이동 종료
                }
                if ((perDY >= 0.0f && y <= defpos.y) || (perDY <= 0.0f && y >= defpos.y))
                {
                    endY = true;//Y방향 이동 종료
                }
                if (defpos.y > transform.position.y)
                    transform.position = defpos;
                //블록 이동
                transform.Translate(new Vector3(-perDX, -perDY, defpos.z));
            }
            /*
            if (isReverse)
            {
                //반대방향 이동
                //이동량이 양수 이동 위치가 초기 위치보다 작거나
                //이동량이 음수고 이동 위치가 초기 위치보다 큰경우
                if ((perDX >= 0.0f && x <= defpos.x) || (perDX <= 0.0f && x >= defpos.x))
                {
                    //이동량이 +
                    endX = true;//X방향 이동 종료
                }
                if ((perDY >= 0.0f && y <= defpos.y) || (perDY <= 0.0f && y >= defpos.y))
                {
                    endY = true;//Y방향 이동 종료
                }
                //블록 이동
                transform.Translate(new Vector3(-perDX, -perDY, defpos.z));
            }
            else
            {
                //정방향이동
                //이동량이 양수고 이동위치가 초기 위치보다 크거나
                //이동량이 음수고 이동 위치가 초기 위치보다 작은경우
                if ((perDX >= 0.0f && x >= defpos.x + moveX) || (perDX < 0.0f && x < defpos.x + moveX))
                {
                    endX = true; //X방향 이동 종료
                }
                if ((perDY >= 0.0f && y >= defpos.y + moveY) || (perDY < 0.0f && y < defpos.y + moveY))//x로 되있길래 y로 바꿧음
                {
                    endY = true; //Y방향 이동 종료
                }
                //블록 이동
                Vector3 v = new Vector3(perDX, perDY, defpos.z);
                transform.Translate(v);

            }

            if (endX && endY)
            {
                //이동 종료
                if (isReverse)
                {
                    //위치가 어긋나는 것을 방지하고자 정면 방향 이동으로 돌아가기전에 초기위치로 돌리기
                    transform.position = defpos;
                }
                isReverse = !isReverse;
                isCanMove = false;
                if (isMoveWhenOn == false)
                {
                    //올라갔을떄 움직이는 값이 꺼진경우
                    Invoke("Up", weight);
                }
            }*/

        }

    }



    public void Up()
    {
        isCanMove = true;


    }

    //이동하지 못하게 만들기

    public void Stop()
    {
        isCanMove = false;
    }

    //충돌체크를 OnCollision으로 처리할 거임
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //접촉한것이 플레이어라면 이동 블록의 자식으로 만들기
            collision.transform.SetParent(transform);

            if (isMoveWhenOn)
            {
                //올라갔을떄 움직이는 경우면
                isCanMove = true; // 이동하게 만들기
            }
        }
    }

    //충돌 종료
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //충돌에서 빠져나간 것이 플레이어라면 이동 블록의 자식에서 제외시키기
            collision.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //    PlayerController playerController = gameObject.GetComponent<PlayerController>();
            // if(playerController.IsGroundDetected())
            CheckedIntake++;

            //To do : physics2D.overlapBoxaLL 플레이어 스크립트 보유중인 애들만
            // isGROUND 체크해서 이즈그라운드 상태인 애들만큼 카운트 
            //트리거 ENTER를 빼고.
            //트리거EXIT를 빼고

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckedIntake--;
        }
    }
}
