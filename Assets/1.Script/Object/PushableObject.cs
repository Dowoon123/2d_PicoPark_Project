using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject :MonoBehaviourPunCallbacks
{
    public bool isPushAble;
    public int NeedPlayers = 0;
    public int currPushPlayers = 0;
    public float speed;

    public List<PlayerController> isLeftPusher = new List<PlayerController>();
    public List<PlayerController> isRightPusher =     new List<PlayerController>();


  
    public LayerMask whatIsPlayer;
    public List<PlayerController> players = new List<PlayerController>();


    #region Components;
    public Collider2D col;
    PhotonView pv;
    #endregion



    public void Awake()
    {
        col = GetComponent<Collider2D>();
        pv = GetComponent<PhotonView>();
        
    }

    public void Update()
    {
    

    }

    public void FixedUpdate()
    {
        CheckBox();

        if(isPushAble)
        {  
            for(int i=0; i<players.Count; ++i)
            {
                players[i].SetVelocity(speed, 0f);
            }
        }
    }



    public void CheckBox()
    {
        var box = Physics2D.OverlapBoxAll(transform.position, col.bounds.size,0, whatIsPlayer);


        players.Clear();
        isLeftPusher.Clear();
        isRightPusher.Clear();

         for(int i=0; i<box.Length;i++)
         {
            var pc = box[i].gameObject.GetComponent<PlayerController>();

            if(pc.currState == pc.State_Push)
            {

                players.Add(pc);

            }
         }


         for(int i=0; i< players.Count; ++i)
         {
            // flipX = false  => ¿ÞÂÊ 
            if (!players[i].GetComponentInChildren<SpriteRenderer>().flipX )
            {
                isLeftPusher.Add(players[i]);
            }
            else if (players[i].GetComponentInChildren<SpriteRenderer>().flipX)
            {
                isRightPusher.Add(players[i]);
            }

         }

         if(isLeftPusher.Count > isRightPusher.Count)
         {
            speed = 1.4f;
            pv.RPC("SetCurrPushInt", RpcTarget.AllBuffered, isLeftPusher.Count);

         }
         else if (isRightPusher.Count > isLeftPusher.Count)
         {
            speed = -1.4f;
            pv.RPC("SetCurrPushInt", RpcTarget.AllBuffered, isRightPusher.Count);
         }
         else if (isRightPusher == isLeftPusher)
         {
            speed = 0f;
            pv.RPC("SetCurrPushInt", RpcTarget.AllBuffered, isLeftPusher.Count);
 
         }

    }


   

    [PunRPC]
    public void SetCurrPushInt(int num)
    {
        currPushPlayers = num;

        if (currPushPlayers == NeedPlayers)
            isPushAble = true;
        else
            isPushAble = false;
    }



}
