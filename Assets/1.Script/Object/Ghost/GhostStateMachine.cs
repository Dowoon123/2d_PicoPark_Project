using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateMachine 
{
    public GhostController ghost;

    public GhostState currentState {  get; set; }

    public void Initialize(GhostState _startState)
    {


        currentState = _startState;
        ghost.GetComponent<PhotonView>().RPC("SetCurrState", RpcTarget.AllBuffered, currentState.state_info);

        currentState.Enter();
    }


    public void ChangeState(GhostState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        ghost.GetComponent<PhotonView>().RPC("SetCurrState", RpcTarget.AllBuffered, currentState.state_info);
        currentState.Enter();
    }
}
