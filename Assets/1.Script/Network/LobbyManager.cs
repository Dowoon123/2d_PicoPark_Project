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
    public GameObject RoomListPanel;
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
        RoomListPanel.SetActive(true);


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
        RoomListPanel.SetActive(false);

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
            Invoke("goingToRoom", 2f);
          //  PhotonNetwork.JoinRoom(currentRoomName);

        }
    }

    public void goingToRoom()
    {
        if(!PhotonNetwork.InRoom)
        PhotonNetwork.JoinRoom(currentRoomName);
    }
    public override void OnJoinedRoom()
    {
      
        //PlayerController _player = null;
        //GameObject GOplayer;
    
        //    GOplayer = PhotonNetwork.Instantiate("Pl/TestPlayer_DonotTouch", new Vector2(-4, -2.5f), Quaternion.identity);


        //    _player = GOplayer.GetComponent<PlayerController>();

        //    Debug.Log("첫 플레이어 생성");
        
      
      
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
             
           
             
            panelObj.transform.parent = RoomListPanel.transform;
            var pos = panelPos.position;
            pos.y -= i * 94 ;
            panelObj.transform.position = pos;
            var panel = panelObj.GetComponent<RoomPanel>();
            panel.Room_currentPlayerCount.text = roomList[i].PlayerCount.ToString() + "/ 4";
          
           // panel.Map_Name.text = "Stage 1-1";
           // panel.Map_subTitle.text = "normal";
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
