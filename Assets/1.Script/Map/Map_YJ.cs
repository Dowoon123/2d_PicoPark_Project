using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map_YJ : Map
{
    public bool isChange;
    public bool ChangeComplete;

    [Header("Ball Spawn")]
    public Vector2[] spawnPoints = new Vector2[4];
    public GameObject ball;
    public int ballCount = 0;  //������ �� ���� 
    private bool isBallSpawned = false;


    public Vector2[] planePos = new Vector2[4];

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        BallSpawn();
    }


    public override void Update()
    {
        base.Update();

        if (!ChangeComplete)
            CheckPlayerXpos();


        if (isChange)
        {
            CharacterChange();
            isChange = false;
            ChangeComplete = true;
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
                    if (PhotonNetwork.IsMasterClient)
                    {
                        //������ Ŭ���̾�Ʈ������ �� �����ϵ���
                        photonView.RPC("BallSpawn", RpcTarget.MasterClient);

                        Debug.Log("�� ����");
                        PhotonNetwork.Instantiate("Object/Ball", spawnPoints[i], Quaternion.identity);
                        ballCount++;
                    }
 
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

    public void Playerplane()
    {
        var actorNum = PhotonNetwork.LocalPlayer.ActorNumber;
        string objName = "";

        switch (PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
                objName = "Pl/planeRed";

                break;
            case 2:
                objName = "Pl/planeBlue";
                break;
            case 3:
                objName = "Pl/planeGreen";
                break;
            case 4:
                objName = "Pl/planePurple";
                break;
            default:
                break;
        }


        var player = PhotonNetwork.Instantiate(objName, planePos[actorNum - 1], Quaternion.identity);

        int id = player.GetPhotonView().ViewID;
        GetComponent<PhotonView>().RPC("AddPlayer", RpcTarget.AllBuffered, id);



        //ī�޶� �Ѿư��Բ�
        if (Camera.main.GetComponent<PhotonView>())
        {
            Camera.main.GetComponent<PhotonView>().RPC("ResetPlayer", RpcTarget.AllBuffered);
            Camera.main.GetComponent<PhotonView>().RPC("AddPlayer", RpcTarget.AllBuffered, id);
        }
    }


    //Ư�� ������ ������ �÷��̾ �ٲ�
    public void CheckPlayerXpos()
    {
        for (int i = 0; i < playerList.Count; ++i)
        {
            if (playerList[i].transform.position.x >= 65)
            {
                isChange = true;

                Debug.Log("65�̻� �÷��̾�");
            }
        }
    }

    public void CharacterChange()
    {
        //���� �÷��̾� ������

        Debug.Log("ü���� ����");
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].SetActive(false);
        }


        Playerplane();

    }


}
