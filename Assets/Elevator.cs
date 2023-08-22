using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    //x축 이동거리
    public float moveX = 0.0f;
    //y축 이동거리
    public float moveY = 0.0f;
    //시간
    public float times = 0.0f;
    //정지시간
    public float weight = 0.0f;
    //올라갔을때 움직이기
    public bool isMoveWhenOn = false;

    //움직임 체크
    public bool isCanMove = true;

    //1프레임당 x 이동 값
    public float perDX;

    //1프레임당 y 이동 값
    float perDY;
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

        if (isMoveWhenOn)
        {
            //처음에는 움직이지 않고 올라가면 움지깅기 시작
            isCanMove = false;
        }
    }


    private void FixedUpdate()
    {
        if (isCanMove)
        {
            //이동중
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
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
                if ((perDY >= 0.0f && y >= defpos.y + moveY) || (perDY < 0.0f && y/*x로 되있길래 y로 바꿧음*/ < defpos.y + moveY))
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
            }
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
}
