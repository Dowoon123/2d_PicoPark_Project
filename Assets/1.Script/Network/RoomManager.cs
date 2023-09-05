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
    public GameObject NickNamePanel;
    public InputField field = null;


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

        Debug.Log("·ë ÀÔÀå" + PhotonNetwork.LocalPlayer.ActorNumber + " ¹ø ");





    

    }

    public override void OnLeftRoom()
    {
        GetComponent<PhotonView>().RPC("RefreshCurrentPlayer", RpcTarget.AllBuffered, -1);

        Debug.Log("·ë ÅðÀå");
    }



    public void SetNickname()
    {
        if (field.text.Length <= 0 || field.text.Length > 8)
        {
            Debug.Log("¤±¤¤¤«");
            return;
        }
          

         PhotonNetwork.LocalPlayer.NickName = field.text;

        Debug.Log("ÀÔ·ÂµÈ ÅØ½ºÆ® °ª  :" + field.text);

        string objName = "";

       switch(PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
                objName = "Pl/Player_Red";

                break;
            case 2:
                objName = "Pl/Player_Blue";
                break;
            case 3:
                objName = "Pl/Player_Green";
                break;
            case 4:
                objName = "Pl/Player_Purple";
                break;
            default:
                break;
        }

   

         var obj =  PhotonNetwork.Instantiate(objName, new Vector3(0, 0, 0), Quaternion.identity);

            obj.GetComponent<PlayerController>().pv.RPC("SetNickName", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);
        



        NickNamePanel.SetActive(false);
    }


    public void onChangeInputField()
    {

    }





}
