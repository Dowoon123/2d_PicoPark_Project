using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Player ���� �����ϱ� ���� �Ŵ���
    // Dictionary�� key value ���� ���ؼ� �÷��̾��� ������ ��ȯ�Ѵ�.
    // �÷��̾��� ������ �ʿ��Ҷ� ���Ͽ����ε� �Ⱦ��ϵ�
    [SerializeField]
    Dictionary<int, Player_> _mPlayer = new Dictionary<int, Player_>();
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

    public void AddPlayer(int ActorNumber, Player_ _player)
    {
        _mPlayer.Add(ActorNumber, _player);

    }

    public Player_ GetPlayerByActorNumber(int actorNumber)
    {
        Player_ player = null;
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
        // ���� �ο����� 
    }

}
