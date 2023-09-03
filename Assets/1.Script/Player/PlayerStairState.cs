using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStairState : PlayerGroundedState
{
    Vector2 offsetPos = Vector2.zero;
    Vector2 beforePos = Vector2.zero;
    int downID;
    public PlayerStairState(PlayerController _player, PlayerStateMachine _stateMachine, string _animBoolName, STATE_INFO _info) : base(_player, _stateMachine, _animBoolName, _info)
    {
    }

    public override void Enter()
    {
        base.Enter();

    


        if(player.downPlayer != null)
        {
           
            offsetPos = new Vector2(player.transform.position.x - player.downPlayer.transform.position.x,
                player.transform.position.y - player.downPlayer.transform.position.y);

             downID = player.downPlayer.GetComponent<PhotonView>().ViewID;
            player.pv.RPC("SetOffset", RpcTarget.AllBuffered, downID, offsetPos);

            Debug.Log("머리위 올라탐. Stair 모드 진입" + " 현재 예상 좌표 : " + offsetPos);


            player.GetComponent<PhotonTransformViewClassic>().enabled = false;


        }
    }

    public override void Exit()
    {
        base.Exit();

        player.pv.RPC("DeSetOffset", RpcTarget.AllBuffered, downID);

        player.GetComponent<PhotonTransformViewClassic>().enabled = true;
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
            player.stateMachine.ChangeState(player.State_move);


        //if(xInput == 0)
        //{
        //    if(player.downPlayer != null)
        //    {

        //        //if (!b_ownerTransfer)
        //        //{
        //        //    //player.pv.RPC("SendOwner", RpcTarget.All, player.downPlayer.GetComponent<PhotonView>().ViewID);
        //        //    //b_ownerTransfer = true;
        //        //    //player.pv.RPC("Test", RpcTarget.All);


        //        //}

        //        Vector2 targetPos = player.downPlayer.transform.position;
        //        targetPos += offsetPos;

        //        if (new Vector2(player.transform.position.x, player.transform.position.y) != targetPos)
        //        {

        //            player.transform.position = targetPos;
        //            player.pv.RPC("SetTransform", RpcTarget.OthersBuffered, targetPos);


        //        }
        //    }

        //}


    


    }
}
