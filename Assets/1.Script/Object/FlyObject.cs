using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Realtime;
using UnityEditorInternal;


public class FlyObject : MonoBehaviourPunCallbacks
{
    public float Speed;
    Animator anim;
    Transform trans;
    SpriteRenderer sr;

    float h;
    float v;

    Rigidbody2D rb;
    PhotonView pv;

    bool isDead;


    [Header("Door")]
    public bool isNearDoor = false;
    bool isInDoor;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
                enterDoor();
        }
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



        //�÷��̾ ���� Ƣ�� ������ �ϴ� ����
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
                if (count == 20)
                {
                    break;
                }
                yield return new WaitForSeconds(0.02f);
            }


        }

        // �װ����� ó��

        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(0.5f);

        ReStart();

    }

    private static void ReStart()
    {
        //���ε�
        PhotonNetwork.LoadLevel("���־�");
    }


    //���� ��
    public void enterDoor()
    {

        if (isNearDoor)

        {
            if (isInDoor)
            {
                Time.timeScale = 1; //�ð� ���� �ӵ��� ����
                sr.GetComponent <SpriteRenderer>().enabled = true;
                isInDoor = false; //������ ����
                sr.enabled = true;

            }
            else
            {
                Time.timeScale = 0; //�ð� ����
                sr.GetComponent<SpriteRenderer>().enabled = true;
                isInDoor = true;
                sr.enabled = false;
            }

        }

    }
}
