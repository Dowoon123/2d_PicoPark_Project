using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_PSJ : Map
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();

        if(isSelectOption == false)
        GetComponent<PhotonView>().RPC("SetSpeed", RpcTarget.AllBuffered);
    }

    public override void Update()
    {
        base.Update();
    }

    [PunRPC]
    public void SetSpeed()
    {

        for (int i = 0; i < playerList.Count; ++i)
        {
            var player = playerList[i].GetComponent<PlayerController>();
            player.moveSpeed = 10f;
            player.jumpForce = 30f; 

        }
    }
}


