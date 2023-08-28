using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject RoomCreatePanel;
    public GameObject RoomPanel;
    public GameObject RoomPanelPrefab;
    public List<GameObject> RoomUi_List = new List<GameObject>();
    public List<RoomInfo> RoomList = new List<RoomInfo>();
    public Transform panelPos;
    public GameObject canvas;
    public string currentRoomName;
    public bool isShowRoomList = false;
    public Button GameJoin;
    public Button GameCreate;
    public Button GameBack;
    public string sceneName;
    void Start()
    {
        Screen.SetResolution(960, 600, false);
        PhotonNetwork.ConnectUsingSettings();  // 포톤클라우드와 연결을 시작하는 메서드 

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    public override void OnConnectedToMaster()// 포톤서버에 성공적으로 접속한 후 호출되는 콜백   
    {
        PhotonNetwork.JoinLobby();
    }



    public void RoomCreatePanelOpen()
    {
        RoomCreatePanel.SetActive(false);
        StartCoroutine(RoomCreateDelay());
        
    }
    IEnumerator RoomCreateDelay()
    {
        yield return new WaitForSeconds(0.5f);
        RoomCreatePanel.SetActive(true); 
    }
    
    public void RoomPanelOpen()
    {
        isShowRoomList = true;
        RoomCreatePanel.SetActive(false);
        GameCreate.GetComponent<Button>().interactable = false;
        GameJoin.GetComponent<Button>().interactable = false;
        
        for (int i = 0; i < RoomUi_List.Count; i++)
        {
            RoomUi_List[i].SetActive(true);
            GameBack.GetComponent<Button>().interactable = true;
            
        }   
    }
    public void RoomPanelBack()
    {
        GameCreate.GetComponent<Button>().interactable = true;
        GameJoin.GetComponent<Button>().interactable = true;
        isShowRoomList = false;
        RoomCreatePanel.SetActive(false);
        for (int i = 0; i < RoomUi_List.Count; i++)
        {
            RoomUi_List[i].SetActive(false);
        }
    }

    //씬 불러오는 함수 Load
    public void RoomPanelHome()
    {

        SceneManager.LoadScene("GameStart");

    }




    public void JoinRoom(string room_name)
    {
        SceneManager.LoadScene("RoomScene");
        currentRoomName = room_name;
    }

    public void RoomCreate()
    {

      
            RoomOptions roomOptions = new RoomOptions(); // 새로운 룸옵션 할당
            roomOptions.MaxPlayers = 4;
            roomOptions.IsVisible = true;

            var txt = RoomCreatePanel.GetComponentInChildren<TMP_InputField>().text;
            var room = PhotonNetwork.CreateRoom(txt, roomOptions, TypedLobby.Default);
             JoinRoom(txt);



    }
   public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

        if (scene.name == "RoomScene")
        {
            PhotonNetwork.JoinRoom(currentRoomName);

        }
    }
    public override void OnJoinedRoom()
    {
      
        PlayerController _player = null;
        GameObject GOplayer;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        { 

            GOplayer = PhotonNetwork.Instantiate("Pl/Players", new Vector2(-4, -2.5f), Quaternion.identity);


            _player = GOplayer.GetComponent<PlayerController>();

            Debug.Log("첫 플레이어 생성");
        }
        else
        {

            GOplayer = PhotonNetwork.Instantiate("Pl/Players", new Vector2(4, -2.5f), Quaternion.identity);
            _player = GOplayer.GetComponent<PlayerController>();
            Debug.Log("두번째 플레이어 생성 ");


        }
        if (_player)
        {
            int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            _player.gameObject.GetPhotonView().TransferOwnership(actorNumber);
            _player.actorNumber = actorNumber;

        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
       
        for (int i=0; i< RoomUi_List.Count; ++i)
        {
            if (RoomUi_List[i] != null)
                Destroy(RoomUi_List[i]);
        }
        RoomUi_List.Clear();

        for(int i=0; i<roomList.Count; i++)
        {
            var panelObj = Instantiate(RoomPanelPrefab,canvas.transform);

            var pos = panelPos.position;
            pos.y -= i * 100;
            panelObj.transform.position = pos;
            var panel = panelObj.GetComponent<RoomPanel>();

            panel.Map_Name.text = "Stage 1-1";
            panel.Map_subTitle.text = "normal";
            panel.Room_Title.text = roomList[i].Name;
            panel.LM = this;
            bool active = isShowRoomList == true ? true : false;
            panelObj.SetActive(active);
            // panel을 보일지말지를 isShowRoomlist  bool 변수에 따라 보일지말지 체크하는것.
            // panel.startButton.onClick.AddListener(() => JoinRoom(roomList[i].Name));
            RoomUi_List.Add(panelObj);
            
        }
    }
  
}
