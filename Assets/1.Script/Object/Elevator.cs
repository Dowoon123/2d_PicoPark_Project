using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    /*
     * �� ��ũ��Ʈ�� ������ �ö󰡴� ���������� ����� ž���
     * ��ũ��Ʈ�� X�� �̵��� ���ϸ� MoveX ����
     * Y�� �̵��� ���ϸ� MoveY ���� �Է��ϸ��.
     * ��) ������ ���θ� �ö󰡴� ��ũ��Ʈ�̸�, ������ �������� ������ ���ڸ��� ���ư�.
     */
   

    [SerializeField] private int CheckedIntake = 0; //����� ���� �ο�

    [SerializeField] private int maxIntake; //���� �Ϸ��� �ִ� �ο� ���Ƿ� ����
    [SerializeField] private List<GameObject> UpSidePlayer;


    [SerializeField] private bool endX = false;
    [SerializeField] private bool endY = false;
    [SerializeField] private Vector3 CheckRect;


    [SerializeField] protected LayerMask whatIsGround;
    //���� WhatIsGround�� Playerüũ �ؾߵ�


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


        if ((maxIntake - CheckedIntake) == 0)
        {
            isCheckIntake = true;

        }
        else
            isCheckIntake = false;//������Ʈ ������ �� ������ �׻� ������ �������� ��� üũ�Ұ���.
        




        //�̵���
        float x = transform.position.x;
        float y = transform.position.y;


        if (isCheckIntake)
        {
            //��� �̵�
            Vector3 v = new Vector3(perDX, perDY, defpos.z);
            transform.Translate(v);
        }
        else if (!isCheckIntake)
        {
           
            if (defpos.y > transform.position.y)
                transform.position = defpos;
            //�������� �ּ� ���̴� = ������ �ִ� ��ġ��.
        
            //��� �̵�
            transform.Translate(new Vector3(-perDX, -perDY, defpos.z));
        }





    }
   

    public void CheckPlayerZone()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, CheckRect, 0, whatIsGround);

        UpSidePlayer.Clear();//�ϴ� ���� ���� ������ ������Ʈ���� �������� ������
        int check = 0;//���� ������ CHECK�� ����
        for (int i = 0; i < colliders.Length; ++i)
        {
            var player = colliders[i].GetComponent<PlayerController>();

            //  PlayerGroundedState state = gameObject.GetComponent<PlayerGroundedState>();

            //if (player.isGround || player.isUpperPlayer)
            // if (player.currState == player.groundState)
            //if (player.currState.GetType() == typeof(PlayerGroundedState))
            if (player.currState is PlayerGroundedState)
                check++;
                
            

            Debug.Log("�ڽ� ���� ���� : " + player.currState.GetType());

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
