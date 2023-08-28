using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    //유령 오브젝트 스크립트
    //플레이어를 바라보는 수가 0일경우 플레이어들을 향해 달려올것임.
    //단, 플레이어가 1명이라고 바라볼 경우, 움직일 수 없음.
    //움직이는 모습과 움직이지 않는 모습을 구현하여 SetActive로 구현할 것임.

    Rigidbody2D rb;
    Transform[] target;
    
   public GameObject idleGhost;
   public GameObject moveGhost;

    private void Start()
    {
       moveGhost.SetActive(false);
    }
    private void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponents<Transform>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    
}
