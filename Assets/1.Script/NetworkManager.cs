using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{



    public int currPlayerIdx;
    public GameObject mainCam;
    // �ʿ��� �Ŵ����� 
    //public Lobby lobbyManager;
    public PlayerManager playerM;


    // �г���ǥ��
    public GameObject NicknameText;


    // �г��� �Է�
    public CanvasGroup NameInputPanel;
    public GameObject input;


    public GameObject cubes;
    public GameObject spawnPos1;
    public GameObject spawnPos2;

    string roomName = "MyRoom";
    int maxPlayers = 4;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();  // ����Ŭ����� ������ �����ϴ� �޼��� 
    }



    public override void OnConnectedToMaster()// ���漭���� ���������� ������ �� ȣ��Ǵ� �ݹ�   
    {
        Debug.Log("��������");
        RoomOptions roomOptions = new RoomOptions(); // ���ο� ��ɼ� �Ҵ�
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom("MyRoomName", roomOptions, TypedLobby.Default);
        //  SetPlayerNickName();



    }


    public override void OnJoinedRoom()
    {


        Debug.Log("�� ���� ��");


        PlayerController _player = null;
        GameObject GOplayer;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            currPlayerIdx = 0;
            GOplayer = PhotonNetwork.Instantiate("Players", spawnPos1.transform.position, Quaternion.identity);
             

            _player = GOplayer.GetComponent<PlayerController>();

            Debug.Log("ù �÷��̾� ����");
        }
        else
        {

            GOplayer = PhotonNetwork.Instantiate("Players", spawnPos2.transform.position, Quaternion.identity);
            _player = GOplayer.GetComponent<PlayerController>();
            Debug.Log("�ι�° �÷��̾� ���� ");
        }


        if (_player)
        {
            int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            _player.gameObject.GetPhotonView().TransferOwnership(actorNumber);

            playerM.AddPlayer(actorNumber, _player);
            _player.actorNumber = actorNumber;
      //      var txt = PhotonNetwork.Instantiate("NickNameText", new Vector3(0, 0, 0), Quaternion.identity);
         //   txt.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;


            playerM.SetCurrentIndex(playerM.GetCurrentIndex() + 1);
           
            //  mainCam.GetComponent<FollowCam>().FollowPlayer = GOplayer;
        }






    }

    public override void OnLeftRoom()
    {


    }

    public void SetPlayerNickName()
    {

        // �׽�Ʈ ���̶� ��Ȱ��ȭ �� �Լ� 
        // �ٽ� Ȱ��ȭ ��Ű�� ������ �Ʒ� ��� �ּ� ���� �� ó�� �κ� ����� �� �Լ� ȣ���� �����Ұ�. 

        /*
        if(input.GetComponent<TMP_InputField>().text.Length <= 0)
        {
            return;
        }
        */

        // �г��� ���� 
        PhotonNetwork.LocalPlayer.NickName = input.GetComponent<TMP_InputField>().text;
        Debug.Log("�÷��̾�" + PhotonNetwork.NickName + " ���� ");



        // �̸��Է� �г� ��Ȱ��ȭ
        NameInputPanel.interactable = false;
        NameInputPanel.blocksRaycasts = false;
        NameInputPanel.alpha = 0.0f;

        RoomOptions roomOptions = new RoomOptions(); // ���ο� ��ɼ� �Ҵ�
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;
        

        PhotonNetwork.JoinOrCreateRoom("MyRoomName", roomOptions, TypedLobby.Default);
    }


}
