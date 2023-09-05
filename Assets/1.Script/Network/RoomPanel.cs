using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    public Text Room_Title;
    public Text Room_currentPlayerCount;
    public Text Map_subTitle;
    public Text Map_Name;

    public Button startButton;

    public LobbyManager LM;

    public void ClickJoinRoomButton()
    {
        LM.JoinRoom(Room_Title.text);
    }

}
