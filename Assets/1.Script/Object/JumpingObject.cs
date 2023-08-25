using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObject : MonoBehaviour
{
    [SerializeField] private GameObject jumpBlock;
    [SerializeField] private Animator anim;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;

    Rigidbody2D rb;

    bool jumping = false; //올라 갔을 때 점프
    bool jumpState = false;



    //발판을 밟았을 때 Y축 방향으로 가할 힘의 크기
    Vector2 jumpBlockPw = new Vector2(0, 20);

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Space")&& !jumpState)
        {
            jumping = true;
            jumpState = true;
        }

        Jump();
    }

    private void Jump()
    {
        if(!jumping)
        {
            rb.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, jumpForce);
            rb.AddForce(jumpVelocity,ForceMode2D.Impulse);
            jumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && rb.velocity.y <0)
        {
            //플레이어와 접촉하면
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //플레이어와 접촉하면

        }
    }

}
