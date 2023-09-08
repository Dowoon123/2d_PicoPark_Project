using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class TestNetwork : MonoBehaviourPunCallbacks
{
    [Header("�ڱ� �ʿ� �� ������Ʈ ���� �� �� ������Ʈ �ְ� �Ʒ� ServerName�� �ڱⰡ���ϴ� �̸��ֱ�")]
    [SerializeField] string ServerName;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.SendRate = 422; // �ʴ� 30�� ������ ����
        PhotonNetwork.SerializationRate = 222; // ����ȭ�� �����͸� �ʴ� 30�� ����

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
        RoomOptions roomOptions = new RoomOptions(); // ���ο� ��ɼ� �Ҵ�
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom(ServerName, roomOptions, TypedLobby.Default);
    }
}
