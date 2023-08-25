using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map1 : Map
{
    public float timeLimit = 30.0f; //30√  


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

    public override void SetMapInfo(string SceneName, string MapName, string MapSubName, Vector2 player1_pos, Vector2 player2_pos, Vector2 player3_pos, Vector2 player4_pos)
    {
        base.SetMapInfo(SceneName, MapName, MapSubName, player1_pos, player2_pos, player3_pos, player4_pos);
        Scene_name = "SampleScene";
        Map_name = "Stage1";
        Map_subTitle = "Test1";
    }
}
