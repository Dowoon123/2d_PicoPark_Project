using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map_YJ : Map
{
    public bool isChange;
    public bool ChangeComplete;

    [Header("Ball Spawn")]
    public Transform[] spawnPoints;
    public GameObject ball;
    public int ballCount = 0;  //������ �� ���� 



    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();

    }


    public override void Update()
    {
        base.Update();

        if(!ChangeComplete)
        CheckPlayerXpos();


        if(isChange)
        {
            CharacterChange();
            isChange = false;
            ChangeComplete = true;
        }
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



        var player = PhotonNetwork.Instantiate(objName, playerPosition[actorNum - 1], Quaternion.identity);

        int id = player.GetPhotonView().ViewID;

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
        for(int i=0;i <playerList.Count; ++i)
        {
           if(playerList[i].transform.position.x >= 65)
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
        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].SetActive(false);
        }


        Playerplane();

    }

    //�� ����
    void BallSpawn()
    {

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (ballCount < 4) //4���� ���� ����
            {

                PhotonNetwork.Instantiate("Object/ball", spawnPoint.position, Quaternion.identity);
                ballCount++;
            }
            else
                break; // 4���� ���� �����Ǹ� �ݺ��� ����
        }

    }

    public override void OnJoinedRoom()
    {
        BallSpawn();
    }
}
