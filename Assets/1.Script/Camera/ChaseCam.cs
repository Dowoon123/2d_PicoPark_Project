using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;


public class ChaseCam : MonoBehaviourPunCallbacks
{
    public GameObject[] Players = new GameObject[4];

    public bool isEdgePlayer;
    public GameObject LeftPlayer;
    public GameObject RightPlayer;

     
    public GameObject Edge_RightPlayer;
    public GameObject Edge_LeftPlayer;
    // Update is called once per frame
    void Update()
    {
        if (Players[0] != null) 
        CheckPlayerPosition();

        CheckIsEdge();

        if(LeftPlayer && RightPlayer)
        CaemraMoveToCenter();
        if (Edge_LeftPlayer || Edge_RightPlayer)
        {

        }

        else if (Edge_LeftPlayer && Edge_RightPlayer)
        {
        
        
        
        
        }

    }
    

    public void CheckIsEdge()
    {
        if(LeftPlayer && RightPlayer)
        {
          
            var leftPos = Camera.main.WorldToViewportPoint(LeftPlayer.transform.position);
            var rightPos = Camera.main.WorldToViewportPoint(RightPlayer.transform.position);

            if (leftPos.x <= 0.1f)
                Edge_LeftPlayer = LeftPlayer;
            else if (leftPos.x > 0.1f)
            {
                Edge_LeftPlayer = null;
            }

            if (rightPos.x >= 0.9f)
                Edge_RightPlayer = RightPlayer;
            else if (rightPos.x < 0.9f)
            {
                Edge_RightPlayer = null;
            }
        }
    }


    // 맨끝 플레이어 를 알아내기 위한 함수. 
    public void CheckPlayerPosition()
    {
        float minX = 100;
        float maxX = 0;
        int minIndex = 0;
        int maxIndex = 0;
        for(int i=0; i <Players.Length; ++i)
        {
            if (Players[i] != null)
            {
                if (Players[i].transform.position.x <= minX)
                {
                    minX = Players[i].transform.position.x;
                    minIndex = i;
                }
            }
           

        }
  
        for (int i = 0; i < Players.Length; ++i)
        {
            if (Players[i] != null)
            {
                if (Players[i].transform.position.x >= maxX)
                {
                    maxX = Players[i].transform.position.x;
                    maxIndex = i;
                }
            }

        }


        LeftPlayer = Players[minIndex];

        RightPlayer = Players[maxIndex];
    }

    [PunRPC]
    public void AddPlayer(int viewID)
    {
        var player = PhotonView.Find(viewID);

        if(player != null)
        {
            for(int i=0; i< Players.Length; ++i)
            {
                if (Players[i] == null)
                {
                    Players[i] = player.gameObject;
                    return;
                }    
            }
        }

    }

    

    public void CaemraMoveToCenter()
    {
        var leftX = LeftPlayer.transform.position.x;
        var rightX = RightPlayer.transform.position.x;

        var center =new Vector3((leftX + rightX) / 2,0,-10);


        transform.position = Vector3.Lerp(transform.position, center, 0.1f);
    }
   
}
