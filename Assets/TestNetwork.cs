using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class TestNetwork : MonoBehaviourPunCallbacks
{
    [Header("자기 맵에 빈 오브젝트 생성 후 이 컴포넌트 넣고 아래 ServerName에 자기가원하는 이름넣기")]
    [SerializeField] string ServerName;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.SendRate = 422; // 초당 30번 데이터 전송
        PhotonNetwork.SerializationRate = 222; // 직렬화된 데이터를 초당 30번 전송

    }

    public override void OnConnectedToMaster()
    {

        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedRoom()
    {
        var player = PhotonNetwork.Instantiate("Pl/Player_Red", new Vector3(1, 2, 0), Quaternion.identity);
        int id = player.GetPhotonView().ViewID;


        if (Camera.main.GetComponent<PhotonView>())
            Camera.main.GetComponent<PhotonView>().RPC("AddPlayer", RpcTarget.AllBuffered, id);
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions(); // 새로운 룸옵션 할당
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom(ServerName, roomOptions, TypedLobby.Default);
    }
}
