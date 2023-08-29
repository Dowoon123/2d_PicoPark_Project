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
    // �г���ǥ��
    public GameObject NicknameText;


    // �г��� �Է�
    public InputField NameInputPanel;
    public GameObject input;
    string playerStr;
    void Info()
    {
        // �г��� ���� 
        PhotonNetwork.LocalPlayer.NickName = input.GetComponent<InputField>().text;
        Debug.Log("�÷��̾�" + PhotonNetwork.NickName + " ���� ");

        if (PhotonNetwork.InRoom)
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                playerStr += PhotonNetwork.PlayerList[i].NickName + ",";
            print(playerStr);

        }


        //�̸��Է� �г� ��Ȱ��ȭ
        NameInputPanel.interactable = false;
        NameInputPanel.blocksRaycasts = false;
        NameInputPanel.alpha = 0.0f;

        RoomOptions roomOptions = new RoomOptions(); // ���ο� ��ɼ� �Ҵ�
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;


        PhotonNetwork.JoinOrCreateRoom("MyRoomName", roomOptions, TypedLobby.Default);

    }

}
