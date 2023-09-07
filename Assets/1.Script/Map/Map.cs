using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;


public class Map : MonoBehaviourPunCallbacks
{
    public float mapTimer;

    public List<GameObject> playerList = new List<GameObject>();

    public Text Timer_Text;
    public GameObject canvasPrefab;
    public GameObject canvas;  

    public string Scene_name = "TestScene";
    public string Map_name = "테스트 맵";
    public string Map_subTitle = "테스트";


    public bool isSelectOption; // 체크해두면 스폰을 진행하지않음.
    public bool isSpawnEnd; // 스폰여부 체크 

    public PhotonView pv;

   public Vector2[] playerPosition = new Vector2[4];
    public  void SetMapInfo(string SceneName, string MapName, string MapSubName,
        Vector2 player1_pos, Vector2 player2_pos, Vector2 player3_pos, Vector2 player4_pos)
    {
        this.Scene_name = SceneName;
        this.Map_name = MapName;
        this.Map_subTitle = MapSubName;

        playerPosition[0] = player1_pos;
        playerPosition[1] = player2_pos;
        playerPosition[2] = player3_pos;
        playerPosition[3] = player4_pos;


    }


   public virtual void Awake()
    {
        //canvas = Instantiate(canvasPrefab);

        //Timer_Text =  canvas.GetComponentInChildren<Text>();

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (PhotonNetwork.IsMasterClient)
        {
         
            if (GetComponent<PhotonView>())
            {
                pv = GetComponent<PhotonView>();
              
                
            }
        }

    }
    public virtual void SpawnTimer()
    {
      canvas = Instantiate(canvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Timer_Text = canvas.GetComponentInChildren<Text>();
    }
   
    public virtual void Start()
    {
        if (!isSelectOption)
        {
            SpawnPlayer();





        }
    }

   public virtual void Update()
    {
        if (Timer_Text)
        {
            mapTimer += Time.deltaTime;


            int minutes = (int)(mapTimer / 60);
            int seconds = (int)(mapTimer % 60);

            string minutesstr = "";
            string secondsstr = "";

            if (minutes < 10)
            {
                minutesstr = "0" + minutes;

            }
            else
                minutesstr = minutes.ToString();

            if (seconds < 10)
            {
                secondsstr = "0" + seconds;
            }
            else
                secondsstr = seconds.ToString();


            Timer_Text.text = "time / " + minutesstr + " : " + secondsstr;
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SpawnPlayer();
        //}
    }


    public virtual void SpawnPlayer()
    {
        var actorNum = PhotonNetwork.LocalPlayer.ActorNumber;
        string objName = "";

        switch (PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
                objName = "Pl/Player_Red";

                break;
            case 2:
                objName = "Pl/Player_Blue";
                break;
            case 3:
                objName = "Pl/Player_Green";
                break;
            case 4:
                objName = "Pl/Player_Purple";
                break;
            default:
                break;
        }



        var player = PhotonNetwork.Instantiate(objName, playerPosition[actorNum-1], Quaternion.identity);

        int id = player.GetPhotonView().ViewID;
        GetComponent<PhotonView>().RPC("AddPlayer", RpcTarget.AllBuffered, id);
       

     


        if (Camera.main.GetComponent<PhotonView>())
           Camera.main.GetComponent<PhotonView>().RPC("AddPlayer", RpcTarget.AllBuffered, id);


    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);



    }

    [PunRPC]
    public void AddPlayer(int viewID)
    {
        var player = PhotonView.Find(viewID);
        playerList.Add(player.gameObject);

        if (playerList.Count == PhotonNetwork.CurrentRoom.PlayerCount)
            isSpawnEnd = true;
    }




}
