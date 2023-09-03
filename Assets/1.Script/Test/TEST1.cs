using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class TEST1 : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

   
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {

     

            GetComponent<PhotonView>().RPC("takeOver", RpcTarget.All);
            
            



        }

    }


    [PunRPC]
    void takeOver()
    {
        //var players = PhotonNetwork.PlayerList;

        //int view1 = players[0].ActorNumber * 1000 + 1;
        //int view2 = players[1].ActorNumber * 1000 + 1;

       // Debug.Log("view 1 ,2 : " + view1 + view2);


        PhotonView targetPhotonView = PhotonView.Find(1001);

        targetPhotonView.TransferOwnership(2001);


    }


}
