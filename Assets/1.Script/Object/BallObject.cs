using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class BallObject : MonoBehaviourPunCallbacks
{
    [SerializeField] float speed; //ball �̵��ӵ� 

    public Rigidbody2D rb;

    float X = 50f;
    float Y = 50f;// ���� ��



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ���� ���ڸ��� �ӵ��� 0���� �༭ �����ְ���
        rb.velocity = Vector3.zero;

    }

    private void Update()
    {
    }

    private void BallMoving()
    {

        ////���� * ���ǵ�� ���� ����
        Vector2 dir = new Vector2(X, Y).normalized;

        rb.velocity = dir * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ////ball�� ���� ������ ball ����
        //if (/*collision.collider.gameObject.layer == 8 &&*/
        //   !collision.collider.gameObject.CompareTag("Brick"))
        //{
        //    PhotonNetwork.Destroy(gameObject);
        //}


        //ball�� brick�� ���� ��� brick ����
        if (collision.collider.CompareTag("Brick"))
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                var BallObj = collision.collider.gameObject;
                var Id = BallObj.GetComponent<PhotonView>().ViewID;

                GetComponent<PhotonView>().RPC("DeleteBrick", RpcTarget.AllBuffered, Id);
            }

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ball�� �÷��̾ ���� ��� ���� ������ 
        if (collision.CompareTag("Player"))
        {
            BallMoving();
        }

        ////ball �Ѿ ��� ���� 
        //if (collision.CompareTag("DeleteZone"))
        //    PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    //������ Ŭ���̾�Ʈ���� ID �����ͼ� ��������
    public void DeleteBrick(int viewID)
    {
        var ball = PhotonView.Find(viewID);

        if (ball.gameObject != null)
        {
            PhotonNetwork.Destroy(ball.gameObject);
        }

    }
}
