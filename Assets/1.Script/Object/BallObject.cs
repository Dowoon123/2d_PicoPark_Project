using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class BallObject : MonoBehaviourPunCallbacks
{
    [SerializeField]float speed; //ball �̵��ӵ� 

    public Rigidbody2D rb;
    float X = 4f;
    float Y = 4f;// ���� ��



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
        rb.velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ////ball�� ���� ������ ball ����
        //if (/*collision.collider.gameObject.layer == 8 &&*/
        //   !collision.collider.gameObject.CompareTag("Brick"))
        //{
        //    Debug.Log("������");
        //    PhotonNetwork.Destroy(gameObject);
        //}


        //ball�� brick�� ���� ��� brick ����
        if (collision.collider.CompareTag("Brick"))
            PhotonNetwork.Destroy(collision.gameObject);


        //ball�� �÷��̾ ���� ��� ���� ������ 
        else if (collision.collider.CompareTag("Player") && !(collision.gameObject))
            BallMoving();
    }
}
