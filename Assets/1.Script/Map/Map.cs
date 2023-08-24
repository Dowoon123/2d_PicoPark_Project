using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public float Timer;

    public Text Timer_Text;
    public GameObject canvasPrefab;

    public GameObject canvas;  

    public string Scene_name = "TestScene";
    public string Map_name = "테스트 맵";
    public string Map_subTitle = "테스트";

 

    public virtual void SetMapInfo(string scenename, string mapname, string subtitle)
    {
        Scene_name = scenename;
        Map_name = mapname;
        Map_subTitle = subtitle;
    }


   public virtual void Awake()
    {
        canvas = Instantiate(canvasPrefab);

        Timer_Text =  canvas.GetComponentInChildren<Text>();

    }
   

   public virtual void Update()
    {
        Timer += Time.deltaTime;


        int minutes = (int)(Timer / 60);
        int seconds = (int)(Timer % 60);

        string MinutesStr = "";
        string SecondsStr = "";

        if (minutes < 10)
        {
            MinutesStr = "0" + minutes;

        }
        else
            MinutesStr = minutes.ToString();

        if (seconds < 10)
        {
            SecondsStr = "0" + seconds;
        }
        else
            SecondsStr = seconds.ToString();


        Timer_Text.text = "Time / "+ MinutesStr + " : " + SecondsStr;
        

    }

}
