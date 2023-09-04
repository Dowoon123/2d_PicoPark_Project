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
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {

        if (pv.IsMine)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(h * Speed, v * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            GameOver();
        }
    }

    private void GameOver()
    {

        anim.SetTrigger("Die");

        rb.velocity = Vector3.zero;
        transform.localScale = new Vector3(3.61999989f, 3.61999989f, 3.61999989f);

    }
}
