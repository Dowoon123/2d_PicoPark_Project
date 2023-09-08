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
    SpriteRenderer sr;

    float h;
    float v;

    Rigidbody2D rb;
    PhotonView pv;

    bool isDead;

    bool canMove = true; // �̵� �������� ���� 

    bool isMoving = true; //�ڵ� �̵� ���� 

    public bool isReadyClear;
    public bool isFrontOfGoal;
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
        //�ڵ� �̵�
        if (isMoving && canMove && !isReadyClear && !isFrontOfGoal )
            transform.Translate(new Vector2(13f * Time.smoothDeltaTime, 0), Space.Self);


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            enterDoor();
        }

        if(isNearDoor)
        {
            isFrontOfGoal = true;
        }
    }
    private void FixedUpdate()
    {
        if (pv.IsMine && !isDead)
        {

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

        }

        else
        {
            h = 0f;
            v = 0f;
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

        //�ݶ��̴� ����
        GetComponent<Collider2D>().enabled = false;


        //�÷��̾ ���� Ƣ�� ������ �ϴ� ����
        StartCoroutine(deadJump());

        canMove = false;
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
                if (count == 5)
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
                if (count == 50)
                {
                    break;
                }
                yield return new WaitForSeconds(0.02f);
            }


        }

        // �װ����� ó��

        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(0.01f);

        canMove = false;
    }



    //���� ��
    public void enterDoor()
    {

        if (isNearDoor)

        {
            if (!isReadyClear)
            {
                GetComponent<PhotonView>().RPC("Set", RpcTarget.All, true);
            


            }
        }
       
         
        if(isReadyClear)
        {
            GetComponent<PhotonView>().RPC("Set", RpcTarget.All, false);
        }

           
        

    }


    [PunRPC]
    public void Set(bool b)
    {
        isReadyClear = b;
        sr.GetComponent<SpriteRenderer>().enabled = !b;
        GetComponent<Collider2D>().enabled = !b;
    }
}
