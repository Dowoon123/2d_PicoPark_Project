using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Realtime;


public class FlyObject : MonoBehaviourPunCallbacks
{
    public float Speed;
    Animator anim;
    Transform trans;

    float h;
    float v;

    Rigidbody2D rb;
    PhotonView pv;

    bool isDead;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (pv.IsMine && !isDead)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        rb.velocity = new Vector2(h * Speed, v * Speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        anim.SetTrigger("Die");
        isDead = true;
        transform.localScale = new Vector3(3.61999989f, 3.61999989f, 3.61999989f);

        GetComponent<Collider2D>().enabled = false;



        //플레이어를 위로 튀어 오르게 하는 연출
        StartCoroutine(deadJump());

    }

    IEnumerator deadJump()
    {
        bool isFall = false;
        int count = 0;
        while (true)
        {
            if (!isFall)
            {


                transform.Translate(Vector2.up);
                count++;
                if (count == 6)
                {
                    isFall = true;
                    count = 0;

                }
                yield return new WaitForSeconds(0.05f);




            }
            else
            {
                transform.Translate(Vector2.down);
                count++;
                if (count == 12)
                {
                    break;
                }
                yield return new WaitForSeconds(0.02f);
            }


        }

        // 죽고난후의 처리

        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(0.5f);

        ReStart();

    }

    private static void ReStart()
    {
        //씬로드
        PhotonNetwork.LoadLevel("윤주씬");
    }
}
