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
    public Text ClearText;
    public GameObject canvasPrefab;
    public GameObject canvas;  

    public string Scene_name = "TestScene";
    public string Map_name = "테스트 맵";
    public string Map_subTitle = "테스트";


    public bool isSelectOption; // 체크해두면 스폰을 진행하지않음.
    public bool isSpawnEnd; // 스폰여부 체크 

    public PhotonView pv;

   public Vector2[] playerPosition = new Vector2[4];

    public bool isClear = false;
    public GameObject Door;
   

    
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
        if(!isSelectOption)
        SpawnTimer();

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

        var texts = canvas.GetComponentsInChildren<Text>();

        for(int i=0; i<texts.Length; i++)
        {
            if (texts[i].name == "Timer")
            {
                Timer_Text = texts[i];
            }
            if (texts[i].name == "Clear")
            {
                ClearText = texts[i];
                ClearText.gameObject.SetActive(false);
            }

        }
    }
   
    public virtual void Start()
    {
        if (!isSelectOption)
        {
            SpawnPlayer();





        }
    }

    public void CheckPlayerAllReady()
    {
        var count = 0;
        for (int i=0; i< playerList.Count; ++i)
        {
            var pc = playerList[i].GetComponent<PlayerController>();
            if (pc.isReadyToClear)
                count++;
        }

        if (count == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            StartCoroutine(ClearMap());
            isClear = true;
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


        if(!isClear && playerList.Count > 0)
        {
            if(PhotonNetwork.IsMasterClient)
            CheckPlayerAllReady();
        }





        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LoadLevel(Scene_name);

        }
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

    [PunRPC]
    public void ResetPlayer(int viewID)
    {
        for(int i =0; i< playerList.Count; i++) 
        {
            playerList[i] = null;
        }
    }


    IEnumerator ClearMap()
    {
        float dist = 1000f;
        ClearText.gameObject.SetActive(true);

        while(true)
        {
            dist = Vector3.Distance(ClearText.transform.localPosition, Vector3.zero);

            if (dist > 10)
            {
                ClearText.transform.Translate(Vector3.right * 10.5f);
                Debug.Log("dist :" + dist);
                yield return new WaitForSeconds(0.03f);
            }
            else
                break;
           

        }

        yield return new WaitForSeconds(4.1f);

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("StageSelectScene");
    }

}
