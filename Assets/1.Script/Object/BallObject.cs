using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class BallObject : MonoBehaviourPunCallbacks
{
    [SerializeField] float speed; //ball 이동속도 

    public Rigidbody2D rb;

    float X = 50f;
    float Y = 50f;// 방향 값


    public LayerMask WhatIsReflectObject;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 스폰 하자마자 속도값 0으로 줘서 멈춰있게함
        rb.velocity = Vector3.zero;

    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ball이 플레이어에 닿은 경우 공이 움직임 
        if (collision.CompareTag("Player"))
        {
            // 플레이어와 충돌한 경우, 공을 발사합니다.
            BallMoving();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ball이 땅에 닿으면 ball 삭제
        if (collision.collider.gameObject.layer == 8 && !collision.collider.gameObject.CompareTag("Brick"))
        {
          //  PhotonNetwork.Destroy(gameObject);
        }

        //ball이 brick에 닿은 경우 brick 삭제
        if (collision.collider.CompareTag("Brick"))
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                var BallObj = collision.collider.gameObject;
                var Id = BallObj.GetComponent<PhotonView>().ViewID;

                GetComponent<PhotonView>().RPC("DeleteBrick", RpcTarget.AllBuffered, Id);
            }
        }

        // ball이 벽에 부딪혔을 때, 반사 벡터를 계산하여 적용합니다.
        //if (collision.collider.gameObject.layer == 9) // Layer 9: 장애물
        //{
        //    Vector2 reflectionVector = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
        //    rb.velocity = reflectionVector * speed;
        //}

        if (collision.collider.gameObject.layer == WhatIsReflectObject)
        { Vector2 reflectionVector = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
        rb.velocity = reflectionVector * speed;
    }

}

    [PunRPC]
    private void BallMoving()
    {
        // 방향 * 스피드로 힘을 가함
        Vector2 dir = new Vector2(X, Y).normalized;
        rb.velocity = dir * speed;
    }


    [PunRPC]
    //마스터 클라이언트에서 ID 가져와서 삭제해줌
    public void DeleteBrick(int viewID)
    {
        var ball = PhotonView.Find(viewID);

        if (ball.gameObject != null)
        {
            PhotonNetwork.Destroy(ball.gameObject);
        }

    }
}
