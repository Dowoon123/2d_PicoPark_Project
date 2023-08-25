using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class RoomManager : MonoBehaviourPunCallbacks
{

    [SerializeField] Text playerCountText;
    public int currentPlayerCount = 0;


    public void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PhotonNetwork.LoadLevel("LobbyScene");
            }
        }
    }

    [PunRPC]
    public void RefreshCurrentPlayer(int num)
    {
        currentPlayerCount += num;

        playerCountText.text = currentPlayerCount.ToString() + " / " + "4";
    }

    public override void OnJoinedRoom()
    {
        GetComponent<PhotonView>().RPC("RefreshCurrentPlayer", RpcTarget.AllBuffered, 1);

        Debug.Log("∑Î ¿‘¿Â");
    }

    public override void OnLeftRoom()
    {
        GetComponent<PhotonView>().RPC("RefreshCurrentPlayer", RpcTarget.AllBuffered, -1);

        Debug.Log("∑Î ≈¿Â");
    }







}
