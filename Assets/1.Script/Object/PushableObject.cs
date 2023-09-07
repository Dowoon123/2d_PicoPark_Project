using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PushableObject :MonoBehaviourPunCallbacks
{
    public bool isPushAble;
    public int NeedPlayers = 0;
    public int currPushPlayers = 0;
    public float speed;

    public List<PlayerController> isLeftPusher = new List<PlayerController>();
    public List<PlayerController> isRightPusher =     new List<PlayerController>();

    public Text condition;
    private int number_remaining;
    private string conditionText;

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
        NeedPlayers = PhotonNetwork.CurrentRoom.PlayerCount;

    }

    public void FixedUpdate()
    {
        CheckBox();

        if(isPushAble)
        {

            transform.Translate(Vector2.right * speed * Time.deltaTime);

            for (int i=0; i<players.Count; ++i)
            {
                players[i].transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }

        number_remaining = NeedPlayers - currPushPlayers;
        conditionText = number_remaining.ToString();
        condition.GetComponent<Text>().text = conditionText;

    }



    public void CheckBox()
    {
        var size = col.bounds.size;
        size.x += 7f;
        var box = Physics2D.OverlapBoxAll(transform.position, size,0, whatIsPlayer);
        Debug.Log(col.bounds.size);

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
            speed = -1.4f;
            pv.RPC("SetCurrPushInt", RpcTarget.AllBuffered, isLeftPusher.Count);

         }
         else if (isRightPusher.Count > isLeftPusher.Count)
         {
            speed = 1.4f;
            pv.RPC("SetCurrPushInt", RpcTarget.AllBuffered, isRightPusher.Count);
         }
         else if (isRightPusher == isLeftPusher)
         {
            speed = 0f;
            pv.RPC("SetCurrPushInt", RpcTarget.AllBuffered, isLeftPusher.Count);
 
         }

         if(isLeftPusher.Count == 0 && isRightPusher.Count ==0)
        {
            speed = 0f;
            pv.RPC("SetCurrPushInt", RpcTarget.AllBuffered, 0);
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
