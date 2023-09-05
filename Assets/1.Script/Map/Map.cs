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
    public float Timer;

    public Text Timer_Text;
    public GameObject canvasPrefab;

    public GameObject canvas;  

    public string Scene_name = "TestScene";
    public string Map_name = "테스트 맵";
    public string Map_subTitle = "테스트";

    Vector2[] playerPosition = new Vector2[4];
    public virtual void SetMapInfo(string SceneName, string MapName, string MapSubName,
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

        

    }
   
    public virtual void Start()
    {
       // SpawnPlayer();
    }

   public virtual void Update()
    {
        //Timer += Time.deltaTime;


        //int minutes = (int)(Timer / 60);
        //int seconds = (int)(Timer % 60);

        //string MinutesStr = "";
        //string SecondsStr = "";

        //if (minutes < 10)
        //{
        //    MinutesStr = "0" + minutes;

        //}
        //else
        //    MinutesStr = minutes.ToString();

        //if (seconds < 10)
        //{
        //    SecondsStr = "0" + seconds;
        //}
        //else
        //    SecondsStr = seconds.ToString();


        //Timer_Text.text = "Time / "+ MinutesStr + " : " + SecondsStr;
        

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            SpawnPlayer();
        }
    }


    public virtual void SpawnPlayer()
    {
        var pl = PhotonNetwork.PlayerList;

        Debug.Log("스폰플레이어 실행됨");
        for(int i=0; i< pl.Length; ++i)
        {
            if (pl[i].IsLocal)
            {
                Debug.Log("플레이어 스폰 시도" + pl[i].UserId +" " +  i);
                PhotonNetwork.Instantiate("Pl/Players", playerPosition[i], Quaternion.identity);

            }
        }
        
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

         
     
    }


}
