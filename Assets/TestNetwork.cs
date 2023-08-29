using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class TestNetwork : MonoBehaviourPunCallbacks
{
    

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.SendRate = 60; // 초당 30번 데이터 전송
        PhotonNetwork.SerializationRate = 60;

    }

    public override void OnConnectedToMaster()
    {

        PhotonNetwork.JoinLobby();
       
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Pl/Test", new Vector3(1, 2, 0), Quaternion.identity);
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions(); // 새로운 룸옵션 할당
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom("DowwooTest", roomOptions, TypedLobby.Default);
    }
}
