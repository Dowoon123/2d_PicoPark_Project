using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Map_YJ : Map
{
  

 

    public override void Start()
    {
        base.Start();
    }

    public override void SpawnPlayer()
    {
        var pl = PhotonNetwork.PlayerList;

        Debug.Log("스폰플레이어 실행됨");
        for (int i = 0; i < pl.Length; ++i)
        {
            if (pl[i].IsLocal)
            {
                Debug.Log("플레이어 스폰 시도" + pl[i].UserId + " " + i);
                PhotonNetwork.Instantiate("Pl/planeBlue", playerPosition[i], Quaternion.identity);
                PhotonNetwork.Instantiate("Pl/planeRed", playerPosition[i], Quaternion.identity);
                PhotonNetwork.Instantiate("Pl/planeGreen", playerPosition[i], Quaternion.identity);
                PhotonNetwork.Instantiate("Pl/planePurple", playerPosition[i], Quaternion.identity);

            }
        }

    }
}
