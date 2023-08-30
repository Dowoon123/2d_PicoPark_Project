using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDownFinite : MonoBehaviour
{
    /*
       * �� ��ũ��Ʈ�� ������ �̵��ϴ� ���������� ��ũ��Ʈ��.
       * ��, Y�� ���� �̵��� ���� ������ ���
       * �Ʒ��� �����Ƽ� Down���� ������.
       * 
       * 
       */
    [Header("����� �ο�")]
    [SerializeField] private int CheckedIntake = 0; //����� �ο�
    [Header("���� �ִ� �ο�(����)")]
    [SerializeField] private int intake; //���� �Ϸ��� �ο� ���Ƿ� ����
    [SerializeField] private List<GameObject> UpSidePlayer;


    [SerializeField] private bool endX = false; //�۵� ���θ� üũ�� �ο� ��������, �̹� ��ũ��Ʈ������ ������� ���� ����.
    [SerializeField] private bool endY = false;
    [Header("�÷��̾ �����ϱ� ���� ����")]
    [SerializeField] private Vector3 CheckRect;//������ ���� ��ŭ üũ �ϱ� ���� ����3 ������.
    [Header("���� ���� ���� ���� ���� y���� ��� �Ʒ��θ���")]
    [SerializeField] private Vector3 arrivePos;//���� ��ġ�� ���� �����ϱ� ���� ����3 ����

    [SerializeField] protected LayerMask whatIsGround;


    //x�� �̵��Ÿ�
    public float moveX = 0.0f;
    //y�� �̵��Ÿ�
    public float moveY = 0.0f;
    //�ð�
    public float times = 0.0f;
    //�����ð�
    public float weight = 0.0f;

    [SerializeField] private bool isCheckIntake = false;
    //�����ο� ������ �����ϴ��� üũ�ϴ� bool��
    //TRUE�� ���������� �۵�


    //1�����Ӵ� x �̵� ��
    public float perDX;
    //1�����Ӵ� y �̵� ��
    public float perDY;

    //�ʱ� ��ġ
    Vector3 defpos;
    //���� ����
    bool isReverse = false;




    void Start()
    {
        //�ʱ���ġ
        defpos = transform.position;
        //1�����ӿ� �̵��ϴ� �ð�
        float timestep = Time.deltaTime;
        //1������ X �̵���
        perDX = moveX / (1.0f / timestep * times);
        //1�������� Y �̵� ��
        perDY = moveY / (1.0f / timestep * times);


    }

    private void Update()
    {
        CheckPlayerZone();
    }

    private void FixedUpdate()
    {


        if ((intake - CheckedIntake) == 0)
        {
            isCheckIntake = true;

        }
        else
            isCheckIntake = false;//������Ʈ ������ �� ������ �׻� ������ �������� ��� üũ�Ұ���.

        if (isCheckIntake)
        {//������(�Ʒ�)�̵�

            Debug.Log("������������");
            //��� �̵�
            Vector3 v = new Vector3(-perDX, -perDY, defpos.z);
            transform.Translate(v);


            if (transform.position.y < arrivePos.y)
                transform.position = new Vector3(transform.position.x, arrivePos.y, transform.position.z);
        }
        else if (!isCheckIntake)
        {

            if (transform.position.y > defpos.y)
                transform.position = defpos;
            //�������� �ּ� ���̴� = ������ �ִ� ��ġ��.

            //��� �̵�
            transform.Translate(new Vector3(perDX, perDY, defpos.z));
        }




    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + new Vector3(0, 2, 0), CheckRect);
        // Gizmos.DrawCube(transform.position + new Vector3(rectXSize, 0,0) , CheckRect);
    }

    public void CheckPlayerZone()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(0, 2, 0), CheckRect, 0, whatIsGround);

        UpSidePlayer.Clear();//�ϴ� ���� ���� ������ ������Ʈ���� �������� ������
        int check = 0;//���� ������ CHECK�� ����
        for (int i = 0; i < colliders.Length; ++i)
        {
            var player = colliders[i].GetComponent<PlayerController>();

            if (player.currState is PlayerGroundedState)
                check++;

            UpSidePlayer.Add(player.gameObject);
            //����Ʈ�� var�� ����� player�� �ᱹ colliders[i]. ����Ʈ�� �Ҵ� ���� ���̴� ����Ʈ�� ����
        }

        CheckedIntake = check; //check�� 0���� ���� ��ǻ� Clear()������.

        if (colliders.Length == 0)
            CheckedIntake = 0;
        //colliders�� ����Ʈ ���� ������. colliders.[0] �� �Ҵ� �ѹ��̶� ������ 1�̴ϱ�.
        //CheckedIntake�� ���� 0���� �ʱ�ȭ

    }

}
