using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map_YJ : Map
{
    //public bool isChange;
    //public bool ChangeComplete;


    public Vector2[] planePos = new Vector2[4];

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {

        if(!isSelectOption)
        Playerplane();
    }


    public override void Update()
    {
        base.Update();

        //if (!ChangeComplete)
        //    CheckPlayerXpos();


        //if (isChange)
        //{
        //    CharacterChange();
        //    isChange = false;
        //    ChangeComplete = true;
        //}
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



        //카메라가 쫓아가게끔
        if (Camera.main.GetComponent<PhotonView>())
        {
            Camera.main.GetComponent<PhotonView>().RPC("AddPlayer", RpcTarget.AllBuffered, id);
        }
    }


    ////특정 구간을 지나면 플레이어가 바뀜
    //public void CheckPlayerXpos()
    //{
    //    for (int i = 0; i < playerList.Count; ++i)
    //    {
    //        if (playerList[i].transform.position.x >= 65)
    //        {
    //            isChange = true;

    //            Debug.Log("65이상 플레이어");
    //        }
    //    }
    //}

    //public void CharacterChange()
    //{
    //    //기존 플레이어 가려줌

    //    Debug.Log("체인지 시작");
    //    for (int i = 0; i < playerList.Count; i++)
    //    {
    //        playerList[i].SetActive(false);
    //    }

    //    Playerplane();

    //}


}
