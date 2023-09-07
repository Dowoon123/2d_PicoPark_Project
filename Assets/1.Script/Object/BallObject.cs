using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class BallObject : MonoBehaviourPunCallbacks
{
    [SerializeField] float speed; //ball 이동속도 

    public Rigidbody2D rb;

    float X = 4f;
    float Y = 4f;// 방향 값



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 스폰 하자마자 속도값 0으로 줘서 멈춰있게함
        rb.velocity = Vector3.zero;

    }

    private void Update()
    {
    }

    private void BallMoving()
    {

        ////방향 * 스피드로 힘을 가함
        Vector2 dir = new Vector2(X, Y).normalized;

        rb.velocity = dir * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ////ball이 땅에 닿으면 ball 삭제
        //if (/*collision.collider.gameObject.layer == 8 &&*/
        //   !collision.collider.gameObject.CompareTag("Brick"))
        //{
        //    PhotonNetwork.Destroy(gameObject);
        //}


        //ball이 brick에 닿은 경우 brick 삭제
        if (collision.collider.CompareTag("Brick"))
            PhotonNetwork.Destroy(collision.gameObject);
            

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ball이 플레이어에 닿은 경우 공이 움직임 
        if (collision.CompareTag("Player"))
        {
            BallMoving();
        }
        else if (collision.CompareTag("DeleteZone"))
        {
            DeleteAllball();
        }
    }


    //공 삭제
    private void DeleteAllball()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            PhotonNetwork.Destroy(ball);
        }
    }

}
