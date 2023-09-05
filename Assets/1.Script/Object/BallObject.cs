using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallObject : MonoBehaviour
{
    [SerializeField][Range(20f, 50f)] float speed = 25f; //ball 이동속도 

    public Rigidbody2D rb;
    float X = 3f;
    float Y = 3f;// 방향 값



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 스폰 하자마자 속도값 0으로 줘서 멈춰있게함
        rb.velocity = Vector3.zero;


    }

    private void BallMoving()
    {

        //방향 * 스피드로 힘을 가함
        Vector2 dir = new Vector2(X, Y).normalized;

        rb.AddForce(dir * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ball이 brick에 닿은 경우 brick 삭제
        if (collision.collider.CompareTag("Brick"))
            collision.gameObject.SetActive(false);

        //ball이 플레이어에 닿은 경우 공이 움직임 
        else if (collision.collider.CompareTag("Player"))
            BallMoving();

        //ball이 땅에 닿으면 ball 삭제
        else if (collision.collider.CompareTag("Ground"))
            Destroy(gameObject);
    }


}
