using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    public float jumpForce; //������ ���� ���� ũ��
   // public float jumpCooldown; //���� ��Ÿ��
    Animator anim;

    public float Force;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹�� ������Ʈ�� �÷��̾����� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

         
              //  Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

                if (player != null)
                {
                if (Force == 0)
                    Force = 50;

                player.jumpForce = Force;
                     player.stateMachine.ChangeState(player.State_Jump);
                player.jumpForce = 35;
                   // //���� �ӵ�
                   // Vector2 velocity = rb.velocity;

                   // // ���� ���� �� ����
                   // velocity.y = jumpForce;

                   // // ����� �ӵ� �����Ͽ� ���� ȿ�� 
                   // rb.velocity = velocity;

                   // //���� ��Ÿ�� ��� �� ���� ������ ���·� ����
                   //// StartCoroutine(JumpCooldown());
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
    //��Ÿ�� ���
    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);

    }*/

}
