using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private Vector3 CheckRect; // 이 스크립트에서 적용하려는 범위를 보기 위해 사용함.
                                                // 이 스크립트에서 적용하려는 범위를 보기 위해 사용함.

    [SerializeField] private Vector3 MoveCheckRect; // 이 벡터 변수는 범위를 이동 시키기 위해 사용함.

    [SerializeField] private Vector3 MovePlayer;//플레이어를 이동 시키기 위한 위치 변수

    [SerializeField] private bool isDeadZone; //이 변수로 오브젝트에 적용된 스크립트를 DeadZone으로 쓸지
                                              //Deadzone이 아니라 위에서 다시 내려오게 만들지 적용할 것임.
                                              //이 버튼만 체크 해제 하면됨.
                                              //체크시 닿으면 플레이어는 죽고 생성되지 않음, 게임 다시 해야함.
                                              //체크 해제시 닿아도 플레이어는 죽지 않고, 특정 위치로 이동되며 다시 내려옴

    [SerializeField] private List<GameObject> InSidePlayer; //안에 들어온 플레이어들을 리스트에 저장해야됨
    [SerializeField] protected LayerMask isPlayer; // 플레이어인지 체크할거임
    //말만 WhatIsGround지 Player체크 해야됨

    [SerializeField] float CoolTime;
    public GameObject safezone;


    void Start()
    {
       
    }

   
    void Update()
    {

        CoolTime -= Time.deltaTime;

        CheckPlayerZone();

        if(!isDeadZone) 
        {
            gameObject.tag = "Ground";
        }
        else if(isDeadZone)
        {
            gameObject.tag = "DeadZone";
        }
    }


    public void OnDrawGizmos() //
    {
        Gizmos.DrawCube(transform.position + MoveCheckRect, CheckRect);
        // Gizmos.DrawCube(transform.position + new Vector3(rectXSize, 0,0) , CheckRect);
    }


    public void CheckPlayerZone()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + MoveCheckRect, CheckRect, 0, isPlayer);

        InSidePlayer.Clear();//일단 전부 지움 어차피 업데이트에서 무한으로 굴러감
     
        for (int i = 0; i < colliders.Length; ++i)
        {
            var player = colliders[i].GetComponent<PlayerController>();

            IEnumerator Death()
            {
                yield return new WaitForSeconds(0.1f);
                player.rb.AddForce(Vector2.up * 45, ForceMode2D.Impulse);
                CoolTime = 1f;
               

            }

            InSidePlayer.Add(player.gameObject);
            //리스트에 var로 조장된 player는 결국 colliders[i]. 리스트에 할당 받은 것이니 리스트에 저장

            if (!isDeadZone)
            {
                player.transform.position = safezone.transform.position;//아닐 경우 닿기만 한다면.
            }
            else if(isDeadZone)
            {
                player.isDead = true;
                StartCoroutine(Death());
                
                if(CoolTime > 0)
                {
                    StopAllCoroutines();
                }
                
              
            }
        }


    }

 

}
