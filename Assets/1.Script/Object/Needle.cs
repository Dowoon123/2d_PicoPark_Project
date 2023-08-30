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
     * 여기서 로직을 좀 특이하게 다뤄야됨, 먼저 플레이어의 상태를 강제로 지정해야됨, 이게 중요함.
     * 
     * 플레이어의 상태가 무엇이든간은 아니지만.
     * 
     * 점프/에어/무빙 상태에서 접촉시 강제로 상태 전환이 필요하고, 동시에 점프로 날려보내야됨.
     * 날려보내면서 조작은 불가능한 상태고 다시 땅에 닿아야만
     * idle로 돌아올 수 있음.
     * 
     * 여기서 날라가는 상태에서도 또 같은 상태를 반복할 거임. 즉, idle로 돌아가지 못한 상태에서 또 바늘에 닿으면
     * 
     * 계속 날라가는 상태임.
     * 
     * 첫번째 방법, 여기서 스테이트 변수를 하나 받아와서 그 변수를 바꾼다(전달한다).
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
            Debug.Log("부딪힘");

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
