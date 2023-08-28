using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerStateMachine
{
    public PlayerController player;
    public PlayerState currentState { get;  set; }


    public void Initialize(PlayerState _startState)
    {
       
     
        currentState = _startState;
        player.GetComponent<PhotonView>().RPC("SetCurrState", RpcTarget.AllBuffered);
        currentState.Enter();
    }


    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        player.GetComponent<PhotonView>().RPC("SetCurrState", RpcTarget.AllBuffered);
        currentState.Enter();
    }




}
