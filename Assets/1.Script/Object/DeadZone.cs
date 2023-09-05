using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private Vector3 CheckRect; // �� ��ũ��Ʈ���� �����Ϸ��� ������ ���� ���� �����.
                                                // �� ��ũ��Ʈ���� �����Ϸ��� ������ ���� ���� �����.

    [SerializeField] private Vector3 MoveCheckRect; // �� ���� ������ ������ �̵� ��Ű�� ���� �����.

    [SerializeField] private Vector3 MovePlayer;//�÷��̾ �̵� ��Ű�� ���� ��ġ ����

    [SerializeField] private bool isDeadZone; //�� ������ ������Ʈ�� ����� ��ũ��Ʈ�� DeadZone���� ����
                                              //Deadzone�� �ƴ϶� ������ �ٽ� �������� ������ ������ ����.
                                              //�� ��ư�� üũ ���� �ϸ��.
                                              //üũ�� ������ �÷��̾�� �װ� �������� ����, ���� �ٽ� �ؾ���.
                                              //üũ ������ ��Ƶ� �÷��̾�� ���� �ʰ�, Ư�� ��ġ�� �̵��Ǹ� �ٽ� ������

    [SerializeField] private List<GameObject> InSidePlayer; //�ȿ� ���� �÷��̾���� ����Ʈ�� �����ؾߵ�
    [SerializeField] protected LayerMask isPlayer; // �÷��̾����� üũ�Ұ���
    //���� WhatIsGround�� Playerüũ �ؾߵ�

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

        InSidePlayer.Clear();//�ϴ� ���� ���� ������ ������Ʈ���� �������� ������
     
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
            //����Ʈ�� var�� ����� player�� �ᱹ colliders[i]. ����Ʈ�� �Ҵ� ���� ���̴� ����Ʈ�� ����

            if (!isDeadZone)
            {
                player.transform.position = safezone.transform.position;//�ƴ� ��� ��⸸ �Ѵٸ�.
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
