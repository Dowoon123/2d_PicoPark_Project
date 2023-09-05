using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallObject : MonoBehaviour
{
    [SerializeField][Range(20f, 50f)] float speed = 25f; //ball �̵��ӵ� 

    public Rigidbody2D rb;
    float X = 3f;
    float Y = 3f;// ���� ��



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ���� ���ڸ��� �ӵ��� 0���� �༭ �����ְ���
        rb.velocity = Vector3.zero;


    }

    private void BallMoving()
    {

        //���� * ���ǵ�� ���� ����
        Vector2 dir = new Vector2(X, Y).normalized;

        rb.AddForce(dir * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ball�� brick�� ���� ��� brick ����
        if (collision.collider.CompareTag("Brick"))
            collision.gameObject.SetActive(false);

        //ball�� �÷��̾ ���� ��� ���� ������ 
        else if (collision.collider.CompareTag("Player"))
            BallMoving();

        //ball�� ���� ������ ball ����
        else if (collision.collider.CompareTag("Ground"))
            Destroy(gameObject);
    }


}
