using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Player 들을 관리하기 위한 매니저
    // Dictionary의 key value 값을 통해서 플레이어의 정보를 반환한다.
    // 플레이어의 정보가 필요할때 쓰일예정인데 안쓰일듯
    [SerializeField]
    Dictionary<int, PlayerController> _mPlayer = new Dictionary<int, PlayerController>();
    int Currindex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateNickName();
    }

    public void AddPlayer(int ActorNumber, PlayerController _player)
    {
        _mPlayer.Add(ActorNumber, _player);

    }

    public PlayerController GetPlayerByActorNumber(int actorNumber)
    {
        PlayerController player = null;
        _mPlayer.TryGetValue(actorNumber, out player);
        return player;
    }



    public void SetCurrentIndex(int idx)
    {
        Currindex = idx;
    }

    public int GetCurrentIndex()
    {
        return Currindex;
    }


    void UpdateNickName()
    {
        // to do 
        // 현재 인원들의 
    }

}
