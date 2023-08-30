using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BallSpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject ball;
    public int ballCount = 0;  //생성된 공 저장 


    private void Update()
    {
        SpawnPlay();
    }

    void SpawnPlay()
    {

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (ballCount < 4) //4c개의 공만 스폰
            {
                Instantiate(ball, spawnPoint.position, Quaternion.identity);
                ballCount++;
            }
            else
                break; // 4개의 공이 생성되면 반복문 종료
        }

    }

}

