using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Dowoon : Map
{
    public GameObject RopePrefab;
    // 해야할 일 :
    // 플레이어 들을 불러와서 밧줄을 달아줌. 
    // Rope Prefab 필요, 로프생성 후 달아주며 로프의 Start는 나 , End는 상대 좌표로 함 
    // 첫번째 플레이어는 두번째플레이어와 디스턴스 조인트연결  두번째는 세번쨰  -> 이렇게 각각연결함 
    // Distance Joint 연결 해야함 . 이중 값을  EnableCollision 켜야하고
    // maxdistanceOnly 를 켜주고, distance값을 적당히 조절 해줄것. 
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();


        if(isSpawnEnd && PhotonNetwork.IsMasterClient)
        {
            Debug.Log("playerList.Count :" + playerList.Count);
            for (int i=0; i<playerList.Count; i++)
            {
                Debug.Log("i :" + i + " playerList.Count-1 = " + (playerList.Count-1));
                if (i != playerList.Count-1)
                {
                   
                    var view_1 = playerList[i].GetComponent<PhotonView>().ViewID;
                    var view_2 = playerList[i + 1].GetComponent<PhotonView>().ViewID;

                    GetComponent<PhotonView>().RPC("SetJoint", RpcTarget.AllBuffered, view_1, view_2);
      
                    Debug.Log("ㄴㅇㅀ");



                }
            }

            GetComponent<PhotonView>().RPC("SetSpeed", RpcTarget.AllBuffered);
            isSpawnEnd = false;
            
        }
    }

    [PunRPC]
    public void SetJoint(int viewId_1, int viewId_2)
    {

        Debug.Log("view 1 :" + viewId_1 + ", " + viewId_2);

        var player_1 = PhotonView.Find(viewId_1);
        var player_2 = PhotonView.Find(viewId_2);

        Debug.Log("Player : " + player_1.gameObject.name + " , " + player_2.gameObject.name);


        var rope = Instantiate(RopePrefab, player_1.transform);
        var ropeComponent = rope.GetComponent<Rope>();

        ropeComponent.startTransform = rope.transform;
        ropeComponent.endTransform = player_2.transform;

        player_1.gameObject.AddComponent<DistanceJoint2D>();
        var distJoint = player_1.GetComponent<DistanceJoint2D>();

        distJoint.enableCollision = true;
        distJoint.connectedBody = player_2.GetComponent<Rigidbody2D>();
      
        distJoint.maxDistanceOnly = true;
        distJoint.autoConfigureDistance = false;

        distJoint.distance = 10f; 

        player_1.GetComponent<PlayerController>().conectedBody = player_2.gameObject;
        player_2.GetComponent<PlayerController>().conectedBody = player_1.gameObject;
    }

    [PunRPC]
    public void SetSpeed()
    {
        for(int i=0; i< playerList.Count; ++i)
        {
            var player = playerList[i].GetComponent<PlayerController>();
            player.moveSpeed = 10f;
            player.isRope = true;
            
        }
    }




}
