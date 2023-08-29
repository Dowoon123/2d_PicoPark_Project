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
    public int ballCount = 0;  //������ �� ���� 


    private void Update()
    {
        SpawnPlay();
    }

    void SpawnPlay()
    {

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (ballCount < 4) //4c���� ���� ����
            {
                Instantiate(ball, spawnPoint.position, Quaternion.identity);
                ballCount++;
            }
            else
                break; // 4���� ���� �����Ǹ� �ݺ��� ����
        }

    }

}

