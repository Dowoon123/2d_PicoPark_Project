using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Map_YJ : Map
{
    List<GameObject> playerList = new List<GameObject>();
    public bool isNext;

    public override void Start()
    {
        base.Start();
    }

    public override void SpawnPlayer()
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
        playerList.Add(player);


    }

    public void CharacterChange()
    {
        //기존 플레이어 가려줌
        var player = PhotonNetwork.Instantiate("Pl/TestPlayer_DonotTouch", new Vector3(1, 2, 0), Quaternion.identity);
        player.SetActive(false);

        SpawnPlayer();

    }


}
