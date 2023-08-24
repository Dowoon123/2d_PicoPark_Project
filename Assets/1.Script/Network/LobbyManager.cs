using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject RoomCreatePanel;
    public GameObject RoomPanelPrefab;
    public List<GameObject> RoomList = new List<GameObject>();
    public Transform panelPos;
    public GameObject canvas;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();  // ����Ŭ����� ������ �����ϴ� �޼��� 
    }



    public override void OnConnectedToMaster()// ���漭���� ���������� ������ �� ȣ��Ǵ� �ݹ�   
    {
        Debug.Log("��������");
        PhotonNetwork.JoinLobby();

    }

   
    public void RoomCreatePanelOpen()
    {
        RoomCreatePanel.SetActive(true);


    }

    public void RoomCreate()
    {

        Debug.Log("�����");
            RoomOptions roomOptions = new RoomOptions(); // ���ο� ��ɼ� �Ҵ�
            roomOptions.MaxPlayers = 4;
            roomOptions.IsVisible = true;

            var txt = RoomCreatePanel.GetComponentInChildren<TMP_InputField>().text;
            var room = PhotonNetwork.CreateRoom(txt, roomOptions, TypedLobby.Default);


        Debug.Log("������Ϸ�" + room);
    }

    public override void OnJoinedLobby()
    {
       
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("�밻��" );
        for (int i=0; i< RoomList.Count; ++i)
        {
            if (RoomList[i] != null)
                Destroy(RoomList[i]);
        }
        RoomList.Clear();

        for(int i=0; i<roomList.Count; i++)
        {
            var panelObj = Instantiate(RoomPanelPrefab,canvas.transform);

            var pos = panelPos.position;
            pos.y -= i * 40;
            panelObj.transform.position = pos;
            var panel = panelObj.GetComponent<RoomPanel>();

            panel.Map_Name.text = "Stage 1-1";
            panel.Map_subTitle.text = "normal";
            panel.Room_Title.text = roomList[i].Name;
        
            RoomList.Add(panelObj);
        }


        Debug.Log("�밻�ſϷ�");
    }
}
