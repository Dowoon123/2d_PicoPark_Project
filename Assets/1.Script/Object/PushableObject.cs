using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject :MonoBehaviourPunCallbacks
{
    public bool isPushAble;
    public int NeedPlayers = 0;
    public int currPushPlayers = 0;
    

    public List<GameObject> isLeftPusher = new List<GameObject>();
    public List<GameObject> isRightPusher =     new List<GameObject>();
    public bool isLeft = false;
    public bool isCenter = false;



    public void Update()
    {
    

    }

    public void CheckPlayers()
    {
        var left = isLeftPusher.Count;
        var right = isRightPusher.Count;

        if (left > right)
        {
            currPushPlayers = left;
            isLeft = true;
        }
        else if (left == right)
        {
            currPushPlayers = left;
            isCenter = true;
        }
        else if (left < right)
        {
            currPushPlayers = right;
            isLeft = false;
        }

        if(currPushPlayers == NeedPlayers)
        {
            isPushAble = true;
        }
    }


    [PunRPC]
    public void AddPusher(int viewId, float xInput)
    {
        var player = PhotonView.Find(viewId);

        if (xInput < 0)
        {
            isLeftPusher.Add(player.gameObject);
        }
        else if (xInput > 0) 
        {
            isRightPusher.Add(player.gameObject);
        }

        CheckPlayers();



    }

    [PunRPC]
    public void RemovePusher(int viewId, float xInput)
    {
        var player = PhotonView.Find(viewId);

       for(int i=0; i<isLeftPusher.Count; ++i)
        {
            if (isLeftPusher[i] == player.gameObject)
            {
                isLeftPusher.RemoveAt(i);
            }
        }
        for (int i = 0; i < isRightPusher.Count; ++i)
        {
            if (isRightPusher[i] == player.gameObject)
            {
                isRightPusher.RemoveAt(i);
            }
        }
        CheckPlayers();
    }



}
