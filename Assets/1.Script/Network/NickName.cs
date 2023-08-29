using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class NickName : MonoBehaviour
{
    // 닉네임표시
    public GameObject NicknameText;


    // 닉네임 입력
    public InputField NameInputPanel;
    public GameObject input;
    string playerStr;
    void Info()
    {
        // 닉네임 설정 
        PhotonNetwork.LocalPlayer.NickName = input.GetComponent<InputField>().text;
        Debug.Log("플레이어" + PhotonNetwork.NickName + " 입장 ");

        if (PhotonNetwork.InRoom)
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                playerStr += PhotonNetwork.PlayerList[i].NickName + ",";
            print(playerStr);

        }


        //이름입력 패널 비활성화
        NameInputPanel.interactable = false;
        NameInputPanel.blocksRaycasts = false;
        NameInputPanel.alpha = 0.0f;

        RoomOptions roomOptions = new RoomOptions(); // 새로운 룸옵션 할당
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;


        PhotonNetwork.JoinOrCreateRoom("MyRoomName", roomOptions, TypedLobby.Default);

    }

}
