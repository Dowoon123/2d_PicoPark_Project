using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Map_Ball : Map
{
    [Header("Ball Spawn")]
    public Vector2[] spawnPoints = new Vector2[4];
    public GameObject ball;
    public int ballCount = 0;  //������ �� ���� 
    private bool isBallSpawned = false;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();

        GetComponent<PhotonView>().RPC("Setting", RpcTarget.AllBuffered);


        if (!isSelectOption)
            if (PhotonNetwork.IsMasterClient)
            {
                BallSpawn();
            }
    }

    [PunRPC]
    public void Setting()
    {
        for (int i = 0; i < playerList.Count; ++i)
        {
            var player = playerList[i].GetComponent<PlayerController>();
            player.moveSpeed = 40f;
            player.jumpForce = 50f;
            player.transform.localScale = new Vector3(11f, 11f, 11f);

        }
    }

    //�� ����
    private void BallSpawn()
    {
        if (!isBallSpawned)
        {
            for (int i = 0; i < spawnPoints.Length; ++i)
            {
                if (ballCount < 4) //4���� ���� ����
                {


                    Debug.Log("�� ����");
                    PhotonNetwork.Instantiate("Object/Ball", spawnPoints[i], Quaternion.identity);
                    ballCount++;


                }
                else
                    break; // 4���� ���� �����Ǹ� �ݺ��� ����
            }
            isBallSpawned = true;
        }
    }

    public override void OnJoinedRoom()
    {
        BallSpawn();
    }

    public override void Update()
    {
        base.Update();

    }
}
