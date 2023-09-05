using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BallSpawnManager : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPoints;
    public GameObject ball;
    public int ballCount = 0;  //생성된 공 저장 


    private void Update()
    {
       // SpawnPlay();
    }


    void PlayerSpawn()
    {

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (ballCount < 4) //4개의 공만 스폰
            {

                PhotonNetwork.Instantiate("Object/ball", spawnPoint.position, Quaternion.identity);
                ballCount++;
            }
            else
                break; // 4개의 공이 생성되면 반복문 종료
        }

    }

    public override void OnJoinedRoom()
    {
        PlayerSpawn();
    }
}

