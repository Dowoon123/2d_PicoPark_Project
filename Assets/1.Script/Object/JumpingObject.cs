using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObject : MonoBehaviour
{
    [SerializeField] float jumpForce = 400f;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpBlockForce = 2f;

    int jumpCount = 1;
    float moveX;

    bool isGround = false;
    bool isJumpBlock = false;
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if(isGround)
        {
            if(jumpCount > 0)
            {
                if (Input.GetButtonDown("Space"))
                {
                    rb.AddForce(Vector2.up * jumpForce);
                    jumpCount--;
                }
            }
            if(isJumpBlock)
            {
                rb.AddForce(new Vector2(0,jumpBlockForce) * jumpForce);
                isJumpBlock = false;
            }
        }

        moveX = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(moveX,rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
            jumpCount = 1;
        }
        if(collision.gameObject.tag == "JumpBlock")
        {
            isJumpBlock = true;
        }
    }


}
