using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkTest : MonoBehaviourPunCallbacks
{
    [SerializeField] Text[] txt;

    [SerializeField] Button btn;
   
    Player[] player;
    int currPlayerIndex = 0;
    bool isReady = false;

    int readyCount = 1;
    //Player[] player;
    public override void OnJoinedRoom()
    {
        GetComponent<PhotonView>().RPC("SyncUI", RpcTarget.AllBuffered);
     



    }

    [PunRPC]
    void SyncUI()
    {
        player = PhotonNetwork.PlayerList;

        for(int i=0; i<player.Length; ++i)
        {
            if (player[i] != null)
            {
                    if (player[i].IsMasterClient)
                    {
                        txt[i].text = " 방장 ";
                        txt[i].gameObject.SetActive(true);
                    }
                if (player[i].IsLocal)
                {
                    currPlayerIndex = i;
                }
            }
        }
    }




    public void OnClick()
    {
        GetComponent<PhotonView>().RPC("Ready", RpcTarget.AllBuffered, currPlayerIndex);
    }

    [PunRPC]
    void Ready(int playerNum)
    {

        if (!player[playerNum].IsMasterClient)
        {

            var b = !isReady;
            isReady = b;

            if (isReady)
                readyCount++;
            else
                readyCount--;

            txt[playerNum].gameObject.SetActive(isReady);
        }


        if (PhotonNetwork.IsMasterClient)
        {
            if (readyCount == player.Length - 1)
            {
                Debug.Log("게임시작 완료");
            }
            else
                Debug.Log("인원 부족");

        }

    }


    
}
