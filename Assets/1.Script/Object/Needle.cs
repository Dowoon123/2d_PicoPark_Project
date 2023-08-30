using Unity.Mathematics;
using UnityEngine;

public class Needle : MonoBehaviour
{


    [SerializeField] Vector3 CheckRect;

    [SerializeField] protected LayerMask IsPlayer;
    public float jumpForce;
 

    private void Start()
    {

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

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - new Vector3(0, 0, 0), CheckRect);
        // Gizmos.DrawCube(transform.position + new Vector3(rectXSize, 0,0) , CheckRect);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("�ε���");

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (CanJump(player))
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

                if (rb != null)
                {

                    player.stateMachine.ChangeState(player.State_Hit);
                   
             

                }
            }


        }
    }
    private bool CanJump(PlayerController player)
    {
        return player.currState == player.State_idle ||
               player.currState == player.State_move ||
               player.currState == player.State_Air
               || player.currState == player.State_Jump;
    }


}
