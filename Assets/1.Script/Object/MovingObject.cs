using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    /// <summary>
    /// 해당 스크립트는 블록에 대한 이동에 관련된 스크립트임 
    /// X,Y축 중 선택해서 이동이 가능하며 둘다 섞어 대각선 이동처럼 표현도 가능함
    /// 단, 스크립트 구현 단계상, X축 이동후 Y축 이동의 복잡한 이동은 구현되지 않았음.
    /// </summary>

    //x이동 거리
    public float moveX = 0.0f;
    //y이동 거리
    public float moveY = 0.0f;
    //시간
    public float times = 0.0f;
    //정지시간
    public float weight = 0.0f;

    //올라갔을때 움직이기
    public bool isMoveWhenOn = false;

    //움직임
    public bool isCanMove = true;
    //1프레임당 X 이동 값
    float perDX;
    //1프레임당 Y 이동 값
    float perDY;
    //초기 위치
    Vector3 defPos;
    //반전여부
    bool isReverse = false;



    void Start()
    {
        //초기 위치
        defPos = transform.position;
        //1프레임에 이동하는 시간
        float timestep = Time.fixedDeltaTime;
        //1프레임의 X 이동 값
        perDX = moveX / (1.0f / timestep * times);
        //1프레임의 Y 이동 값
        perDY = moveY / (1.0f / timestep * times);

        if (isMoveWhenOn)
        {
            //처음에는 움직이지 않고 올라가면 움직이기 시작
            isCanMove = false;
        }
    }


    void FixedUpdate()
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
                //이동량이 양수고 이동 위치가 초기 위치보다 작거나
                //이동량이 음수고 이동 위치가 초기 위치보다 큰경우
                if ((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                {
                    //이동량이 +
                    endX = true; //X방향 이동 종료
                }
                if ((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f && y >= defPos.y))
                {
                    endY = true; //Y방향 이동 종료
                }
                //블록 이동
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));

            }
            else
            {
                //정방향이동
                //이동량이 양수고 이동 위치가 초기 위치보다 크거나
                //이동량이 음수고 이동 위치가 초기 위치보다 작은경우
                if ((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x < defPos.x + moveX))
                {
                    endX = true; //X방향 이동 종료
                }
                if ((perDY >= 0.0f && y >= defPos.y + moveY) || (perDY < 0.0f && x < defPos.y + moveY))
                {
                    endY = true; //X방향 이동 종료
                }
                //블록 이동
                Vector3 v = new Vector3(perDX, perDY, defPos.z);
                transform.Translate(v);
            }

            if (endX && endY)
            {
                //이동 종료
                if (isReverse)
                {
                    //위치가 어긋나는 것을 방지하고자 정면 방향 이동으로 돌아가기 전에 
                    //초기 위치로 돌리기
                    transform.position = defPos;
                }
                isReverse = !isReverse; //값을 반전시키기
                isCanMove = false;  //이동 가능 값을 false
                if (isMoveWhenOn == false)
                {
                    //올라갔을 때  움직이는 값이 꺼진 경우
                    Invoke("Move", weight); //weight만큼 지연 후 다시 이동
                }
            }
        }
    }

    //이동하게 만들기  
    public void Move()
    {
        isCanMove = true;
    }


    //이동하지 못하게 만들기
    public void Stop()
    {
        isCanMove = false;
    }

    //충돌
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //접촉한 것이 플레이어라면 이동 블록의 자식으로 만들기
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                //올라갔을때 움직이는 경우면
                isCanMove = true; //이동하게 만들기
            }
        }
    }

    //충돌 종료
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //접촉한 것이 플레이어라면 이동 블록의 자식에서 제외시키기
            collision.transform.SetParent(null);
        }
    }


}
