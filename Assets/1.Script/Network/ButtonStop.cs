using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStop : MonoBehaviour
{
    public Button btn; 
    LobbyManager lobbyManager = new LobbyManager();
    public void RoomCreatePanelOpen()
    {

        StartCoroutine(RoomCreateDelay());
        //StopCoroutine(RoomCreateDelay());



    }
    IEnumerator RoomCreateDelay()
    {
        yield return new WaitForSeconds(0.5f);
        lobbyManager.RoomCreatePanel.SetActive(true);
        GameObject.Find("PicoPark").transform.Translate(0, 200, 0);
        btn.GetComponent<Button>().interactable = false;


    }
}
