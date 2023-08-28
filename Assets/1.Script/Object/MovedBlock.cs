using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;


public class MovedBlock : MonoBehaviourPunCallbacks
{
    [Header("����� �ο�")]
    /// <summary>
    /// ����� �ο�
    /// </summary>
    [SerializeField] private int CheckedPushingP = 0;
  
    [Header("���� �ִ� �ο�(����)")]
    /// <summary>
    /// ���� �Ϸ��� �ִ��ο� ���Ƿ� ����
    /// </summary>
    [SerializeField] private int CheckedPushAllPlayer;
 
    [SerializeField] private int facingDirCheck;
    [SerializeField] private List<GameObject> PushPlayer;
    [SerializeField] private Vector3 CheckRect;
    private Rigidbody2D rb;





    [SerializeField] protected LayerMask whatIsGround;

    [SerializeField] private bool isCheckPush = false;
    //�����ο� ������ �����ϴ��� üũ�ϴ� bool��
    //TRUE�� ���������� �۵�


    //x�� �̵��Ÿ�
    public float moveX = 0.0f;
    //y�� �̵��Ÿ�
    public float moveY = 0.0f;

    public float rectXSize = 0;
    //�ð�
    public float times = 0.0f;
    //�����ð�
    public float weight = 0.0f;




    //1�����Ӵ� x �̵� ��
    public float perDX;
    //1�����Ӵ� y �̵� ��
    public float perDY;

    //�ʱ� ��ġ
    Vector3 defpos;
    //���� ����
    bool isReverse = false;


    // Start is called before the first frame update
    void Start()
    {
        // OnDrawGizmos();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckPlayerZone();

        var checkDirection = CheckedPushAllPlayer - facingDirCheck;
        var checkDirection2 = CheckedPushAllPlayer - Mathf.Abs(facingDirCheck);

        if (!isCheckPush)
        {
            if (CheckedPushAllPlayer == CheckedPushingP && checkDirection == 0)
            {
                GetComponent<PhotonView>().RPC("CheckPush", RpcTarget.AllBuffered, true);

            }
            else if ((CheckedPushAllPlayer - CheckedPushingP) == 0 && checkDirection2 == 0)
            {
                GetComponent<PhotonView>().RPC("CheckPush", RpcTarget.AllBuffered, true);
            }

        }

        if (CheckedPushAllPlayer != CheckedPushingP)
            GetComponent<PhotonView>().RPC("CheckPush", RpcTarget.AllBuffered, false);



        if (isCheckPush && facingDirCheck == CheckedPushAllPlayer)
        {
            rb.velocity = new Vector2(moveX, 0);
            Debug.Log("���ʿ��� �и��� ����.");
        }

        if (isCheckPush && -(facingDirCheck) == CheckedPushAllPlayer)
        {
            rb.velocity = new Vector2(-moveX, 0);
            Debug.Log("�����ʿ��� �и��� ����.");
        }



    }


    [PunRPC]
    public void CheckPush(bool b)
    {
        isCheckPush = b;

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - new Vector3(0, 0, 0), CheckRect);
        // Gizmos.DrawCube(transform.position + new Vector3(rectXSize, 0,0) , CheckRect);
    }

    public void CheckPlayerZone()
    {


        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, CheckRect, 0, whatIsGround);


        PushPlayer.Clear();//�ϴ� ���� ���� ������ ������Ʈ���� �������� ������
        int check = 0;//���� ������ CHECK�� ����
        int facingcheck = 0;//���� ������ facingcheck ����
        for (int i = 0; i < colliders.Length; ++i)
        {
            var player = colliders[i].GetComponent<PlayerController>();

            if (player.currState == player.State_Push && player.facingDir > 0)
            {
                check++;
                Debug.Log("�����ʿ��� ���� �Ǿ���");

                if (player.facingDir < 0)
                {
                    facingcheck++;

                }
                else if (player.facingDir > 0)
                {
                    facingcheck--;
                }


            }
            else if (player.stateMachine.currentState == player.State_Push && player.facingDir < 0)
            {
                check++;
                Debug.Log("���ʿ��� ���� �Ǿ���");


                if (player.facingDir > 0)
                {

                    facingcheck--;
                }
                else if (player.facingDir < 0)
                {
                    facingcheck++;
                }
            }


            PushPlayer.Add(player.gameObject);
            //����Ʈ�� var�� ����� player�� �ᱹ colliders[i]. ����Ʈ�� �Ҵ� ���� ���̴� ����Ʈ�� ����

        }

        facingDirCheck = facingcheck;

        CheckedPushingP = check; //check�� 0���� ���� ��ǻ� Clear()������.

        if (colliders.Length == 0)
        {
            CheckedPushingP = 0;
            facingDirCheck = 0;
        }

        //colliders�� ����Ʈ ���� ������. colliders.[0] �� �Ҵ� �ѹ��̶� ������ 1�̴ϱ�.
        //CheckedIntake�� ���� 0���� �ʱ�ȭ



        //Collider2D[] collidersR = Physics2D.OverlapBoxAll(transform.position, CheckRect, 0, whatIsGround);
    }
}
