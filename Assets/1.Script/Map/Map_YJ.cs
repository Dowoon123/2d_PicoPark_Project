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
    public int ballCount = 0;  //생성된 공 저장 



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

        //카메라가 쫓아가게끔
        if (Camera.main.GetComponent<PhotonView>())
        {
            Camera.main.GetComponent<PhotonView>().RPC("ResetPlayer", RpcTarget.AllBuffered);
            Camera.main.GetComponent<PhotonView>().RPC("AddPlayer", RpcTarget.AllBuffered, id);
        }
    }


    //특정 구간을 지나면 플레이어가 바뀜
    public void CheckPlayerXpos()
    {
        for(int i=0;i <playerList.Count; ++i)
        {
           if(playerList[i].transform.position.x >= 65)
            {
                isChange = true;

                Debug.Log("65이상 플레이어");
            }
        }
    }

    public void CharacterChange()
    {
        //기존 플레이어 가려줌

        Debug.Log("체인지 시작");
        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].SetActive(false);
        }


        Playerplane();

    }

    //공 스폰
    void BallSpawn()
    {

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (ballCount < 4) //4개의 공만 스폰
            {

                PhotonNetwork.Instantiate("Object/ball", spawnPoint.position, Quaternion.identity);
                ballCount++;
            }
            else
                break; // 4개의 공이 생성되면 반복문 종료
        }

    }

    public override void OnJoinedRoom()
    {
        BallSpawn();
    }
}
