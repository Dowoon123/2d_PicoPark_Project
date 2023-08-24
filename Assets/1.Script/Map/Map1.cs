using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map1 : Map
{
    public float timeLimit = 30.0f; //30√  
    public override void SetMapInfo(string scenename, string mapname, string subtitle)
    {
        Scene_name = "SampleScene";
        Map_name = "Stage1";
        Map_subTitle = "Test1";
    }

    public override void Awake()
    {
        base.Awake();

        Timer = timeLimit;
    }
    public override void Update()
    {
        base.Update();

        if(Timer <= 0)
        {
            Debug.Log("Time's up!");
        }
    }
}
