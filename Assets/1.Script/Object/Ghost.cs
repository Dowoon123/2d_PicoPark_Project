using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    //���� ������Ʈ ��ũ��Ʈ
    //�÷��̾ �ٶ󺸴� ���� 0�ϰ�� �÷��̾���� ���� �޷��ð���.
    //��, �÷��̾ 1���̶�� �ٶ� ���, ������ �� ����.
    //�����̴� ����� �������� �ʴ� ����� �����Ͽ� SetActive�� ������ ����.

    Rigidbody2D rb;
    Transform[] target;
    
   public GameObject idleGhost;
   public GameObject moveGhost;

    private void Start()
    {
       moveGhost.SetActive(false);
    }
    private void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponents<Transform>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    
}
