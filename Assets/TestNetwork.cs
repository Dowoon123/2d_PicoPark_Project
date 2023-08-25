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


        
    }

    public override void OnConnectedToMaster()
    {

        PhotonNetwork.JoinLobby();
       
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Pl/Players", new Vector3(1, 2, 0), Quaternion.identity);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
}