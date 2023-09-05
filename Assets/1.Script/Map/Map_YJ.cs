using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Map_YJ : Map
{
    [SerializeField] Vector2[] playerPosition = new Vector2[4];

    public override void SetMapInfo(string SceneName, string MapName, string MapSubName, Vector2 player1_pos, Vector2 player2_pos, Vector2 player3_pos, Vector2 player4_pos)
    {
        base.SetMapInfo(SceneName, MapName, MapSubName, player1_pos, player2_pos, player3_pos, player4_pos);

        this.Scene_name = SceneName;
        this.Map_name = MapName;
        this.Map_subTitle = MapSubName;

        playerPosition[0] = player1_pos;
        playerPosition[1] = player2_pos;
        playerPosition[2] = player3_pos;
        playerPosition[3] = player4_pos;
    }

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
