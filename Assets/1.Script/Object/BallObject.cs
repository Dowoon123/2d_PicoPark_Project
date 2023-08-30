using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallObject : MonoBehaviour
{
    [SerializeField][Range(20f, 50f)] float speed = 25f; //ball 이동속도 

    public Rigidbody2D rb;
    float randomX, randomY; // 방향 랜덤 값



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.zero;


    }

    private void BallMoving()
    {
        randomX = Random.Range(-1f, 1);
        randomY = Random.Range(-1f, 1);


        //방향 * 스피드로 힘을 가함
        Vector2 dir = new Vector2(randomX, randomY).normalized;

        rb.AddForce(dir * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Brick"))
            collision.gameObject.SetActive(false);

        else if (collision.collider.CompareTag("Player"))
            BallMoving();

        else if (collision.collider.CompareTag("Ground"))
            Destroy(gameObject);
    }


}
