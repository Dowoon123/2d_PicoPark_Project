using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Player_ : MonoBehaviour
{

    // 플레이어의 정보를 담고 있다. 이 정보는
    // 닉네임, 닉네임 UI Text , 그리고 Player가 보유한 조작가능한 오브젝트를 가지고 있다. 




    GameObject PlayerAbleCharacter;
    string nickname;
    GameObject NicknameText;

    int _actorNumber;
    public int actorNumber
    {
        get { return _actorNumber; }
        set { _actorNumber = value; }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            var h = Input.GetAxisRaw("Horizontal");

            // if(PhotonNetwork)
            transform.Translate(Vector2.right * h * 5.5f * Time.deltaTime);
        }

     
  
    }


    public void SetPlayerCharacter(GameObject obj)
    {
        PlayerAbleCharacter = obj;
    }
    public GameObject GetPlayerCharacter()
    {
        return PlayerAbleCharacter;
    }


    public void SetPlayerNickName(string nick)
    {
        nickname = nick;
    }
    public string GetPlayerNickName()
    {
        return nickname;
    }

    public void SetPlayerNickNameText(GameObject nick)
    {
        NicknameText = nick;
    }
    public GameObject GetPlayerNickNameText()
    {
        return NicknameText;
    }


}
