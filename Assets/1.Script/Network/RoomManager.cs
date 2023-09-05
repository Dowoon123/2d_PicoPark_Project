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
    public List<GameObject> Player = new List<GameObject>();
    public Text StartText;

    public void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PhotonNetwork.LoadLevel("StageSelectScene");
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
        UpdatePlayer();
        Debug.Log("·ë ÀÔÀå" + PhotonNetwork.LocalPlayer.ActorNumber + " ¹ø ");





    

    }

    public override void OnLeftRoom()
    {
        GetComponent<PhotonView>().RPC("RefreshCurrentPlayer", RpcTarget.AllBuffered, -1);

        Debug.Log("·ë ÅðÀå");
    }



    public void UpdatePlayer()
    {
     

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

   

    var p =  PhotonNetwork.Instantiate(objName, new Vector3(0, 0, 0), Quaternion.identity);


        GetComponent<PhotonView>().RPC("SetArray", RpcTarget.AllBufferedViaServer, p.GetComponent<PhotonView>().ViewID);

        if (PhotonNetwork.IsMasterClient)
            StartText.gameObject.SetActive(true);

       // GetComponent<PhotonView>().RPC("SetNick", RpcTarget.All);
    }


    [PunRPC]
    public void SetArray(int view)
    {
        var p = PhotonView.Find(view);

        Player.Add(p.gameObject);

       

    }

    [PunRPC] 
    public void SetNick()
    {
        if (Player.Count > 0)
        {
            for (int i = 0; i < Player.Count; ++i)
            {
                if (Player[i] != null)
                {
                    var Pc = Player[i].GetComponent<PlayerController>();
                    Debug.Log("ÇÃ·¹ÀÌ¾î " + i + "¹øÂ°  "+ Pc.pv.Owner.NickName);
                    Pc.stateTxt.text = Pc.pv.Owner.NickName;
                }


            }
        }
    }





}
