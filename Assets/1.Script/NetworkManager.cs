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
    // 필요한 매니저들 
    //public Lobby lobbyManager;
    public PlayerManager playerM;


    // 닉네임표시
    public GameObject NicknameText;


    // 닉네임 입력
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
        PhotonNetwork.ConnectUsingSettings();  // 포톤클라우드와 연결을 시작하는 메서드 
    }



    public override void OnConnectedToMaster()// 포톤서버에 성공적으로 접속한 후 호출되는 콜백   
    {
        Debug.Log("서버접속");
        RoomOptions roomOptions = new RoomOptions(); // 새로운 룸옵션 할당
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom("MyRoomName", roomOptions, TypedLobby.Default);
        //  SetPlayerNickName();



    }


    public override void OnJoinedRoom()
    {


        Debug.Log("방 접속 됨");


        PlayerController _player = null;
        GameObject GOplayer;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            currPlayerIdx = 0;
            GOplayer = PhotonNetwork.Instantiate("Players", spawnPos1.transform.position, Quaternion.identity);
             

            _player = GOplayer.GetComponent<PlayerController>();

            Debug.Log("첫 플레이어 생성");
        }
        else
        {

            GOplayer = PhotonNetwork.Instantiate("Players", spawnPos2.transform.position, Quaternion.identity);
            _player = GOplayer.GetComponent<PlayerController>();
            Debug.Log("두번째 플레이어 생성 ");
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

        // 테스트 중이라 비활성화 한 함수 
        // 다시 활성화 시키고 싶으면 아래 ↓↓ 주석 제거 후 처음 로비 입장시 이 함수 호출을 제거할것. 

        /*
        if(input.GetComponent<TMP_InputField>().text.Length <= 0)
        {
            return;
        }
        */

        // 닉네임 설정 
        PhotonNetwork.LocalPlayer.NickName = input.GetComponent<TMP_InputField>().text;
        Debug.Log("플레이어" + PhotonNetwork.NickName + " 입장 ");



        // 이름입력 패널 비활성화
        NameInputPanel.interactable = false;
        NameInputPanel.blocksRaycasts = false;
        NameInputPanel.alpha = 0.0f;

        RoomOptions roomOptions = new RoomOptions(); // 새로운 룸옵션 할당
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;
        

        PhotonNetwork.JoinOrCreateRoom("MyRoomName", roomOptions, TypedLobby.Default);
    }


}
