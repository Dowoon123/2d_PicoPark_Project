using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{

    PlayerController player;

    [SerializeField] Vector3 CheckRect;

    [SerializeField] protected LayerMask IsPlayer;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }



    /*
     * ���⼭ ������ �� Ư���ϰ� �ٷ�ߵ�, ���� �÷��̾��� ���¸� ������ �����ؾߵ�, �̰� �߿���.
     * 
     * �÷��̾��� ���°� �����̵簣�� �ƴ�����.
     * 
     * ����/����/���� ���¿��� ���˽� ������ ���� ��ȯ�� �ʿ��ϰ�, ���ÿ� ������ ���������ߵ�.
     * ���������鼭 ������ �Ұ����� ���°� �ٽ� ���� ��ƾ߸�
     * idle�� ���ƿ� �� ����.
     * 
     * ���⼭ ���󰡴� ���¿����� �� ���� ���¸� �ݺ��� ����. ��, idle�� ���ư��� ���� ���¿��� �� �ٴÿ� ������
     * 
     * ��� ���󰡴� ������.
     * 
     * ù��° ���, ���⼭ ������Ʈ ������ �ϳ� �޾ƿͼ� �� ������ �ٲ۴�(�����Ѵ�).
     * 
     */

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, CheckRect, 0, IsPlayer);

        for (int i = 0; i < colliders.Length; ++i)
        {
            var player = colliders[i].GetComponent<PlayerController>();
          
            player.isGimmicked = true;
            
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - new Vector3(0, 0, 0), CheckRect);
        // Gizmos.DrawCube(transform.position + new Vector3(rectXSize, 0,0) , CheckRect);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �����Ǿ���");


        
              //  player.currState = player.State_Hit;
            

        }
    }



}
