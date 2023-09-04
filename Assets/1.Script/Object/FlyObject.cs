using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class FlyObject : MonoBehaviour
{
    public float Speed;
    public Animator anim;

    float h;
    float v;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(h * Speed, v * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            GameOver();
        }
    }

    private void GameOver()
    {
        anim.SetBool("Die", true);
        
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector3.zero;

    }
}
