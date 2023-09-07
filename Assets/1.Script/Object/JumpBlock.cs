using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    public float jumpForce; //점프에 사용될 힘의 크기
   // public float jumpCooldown; //점프 쿨타임
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌한 오브젝트가 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

         
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

                if (rb != null)
                {

                    //현재 속도
                    Vector2 velocity = rb.velocity;

                    // 위쪽 점프 힘 설정
                    velocity.y = jumpForce;

                    // 변경된 속도 적용하여 점프 효과 
                    rb.velocity = velocity;

                    //점프 쿨타임 대기 후 점프 가능한 상태로 변경
                   // StartCoroutine(JumpCooldown());
                }
            
        }
    }
    
    /*
    private bool CanJump(PlayerController player)
    {
        return player.currState == player.State_idle ||
               player.currState == player.State_move ||
               player.currState == player.State_Air ||
               player.currState == player.State_Jump ||
               player.currState == player.State_Stair;
       
    }
    */
    /*
    //쿨타임 대기
    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);

    }*/

}
