using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    /// <summary>
    /// ������°�, �ο��� üũ�ϰ�, �� �ο��� �������°� �ƴϰ� ���� �ȿ��� �ְ�,
    /// �� �� ������ �����Ǹ� ���������� �۵�
    /// �װ� �ƴҰ�� �ϰ�, �������� ����鼭�� ������.
    /// 
    /// ���⼭ �����ؾߵǴ°�, ���ư��� ������ ���� ������ �ʿ���.
    /// �÷��̾ ���� ������, �÷��̾��� ���� �ƴҶ��� ������ reverse�Ǿ���.
    /// 
    /// ������ �۵� ���Ǹ� ä������ ������ �� �Ʒ��� �̵���(�ڵ� �̵��̴ϱ�)
    /// 
    /// ���ľ� �ɰ�. 
    /// 
    /// 1. ���� �����ϸ� ��ٸ� �Ŀ� �۵��� �ȵǾ���, ������ �Ͻ� ����.
    /// 
    /// 2. �� �ڸ����� ��� ��� ������.
    /// 
    /// �̰� ���� �����Ǿ���.
    /// 
    /// 
    /// bool isCheckIntake  üũ�� ���� ����� ������.
    /// ��� ��� �ݵ�� ������ ������ ���Ѿߵ�.
    /// 
    /// ���� �ö󰡰� �������°� �����Ǿ�����,
    /// Stop���°� ����.
    /// �װ� �� �־�ߵ�.
    /// ������ ���� �־ ��ġ ������ ���� ��ų�� ������.
    /// 
    /// </summary>

    [SerializeField] private int CheckedIntake = 0; //����� �ο�

    [SerializeField] private int intake; //���� �Ϸ��� �ο� ���Ƿ� ����
    [SerializeField] private List<GameObject> UpSidePlayer;


    [SerializeField] private bool endX = false;
    [SerializeField] private bool endY = false;
    [SerializeField] private Vector3 CheckRect;


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




        //�̵���
        float x = transform.position.x;
        float y = transform.position.y;


        if (isCheckIntake)
        {//�������̵�
         //�̵����� ����� �̵���ġ�� �ʱ� ��ġ���� ũ�ų�
         //�̵����� ������ �̵� ��ġ�� �ʱ� ��ġ���� �������
            if ((perDX >= 0.0f && x >= defpos.x + moveX) || (perDX < 0.0f && x < defpos.x + moveX))
            {
                endX = true; //X���� �̵� ����
            }
            if ((perDY >= 0.0f && y >= defpos.y + moveY) || (perDY < 0.0f && y < defpos.y + moveY))//x�� ���ֱ淡 y�� �مf��
            {
                endY = true; //Y���� �̵� ����
            }
            //��� �̵�
            Vector3 v = new Vector3(perDX, perDY, defpos.z);
            transform.Translate(v);
        }
        else if (!isCheckIntake)
        {
            if ((perDX >= 0.0f && x <= defpos.x) || (perDX <= 0.0f && x >= defpos.x))
            {
                //�̵����� +
                endX = true;//X���� �̵� ����
            }
            if ((perDY >= 0.0f && y <= defpos.y) || (perDY <= 0.0f && y >= defpos.y))
            {
                endY = true;//Y���� �̵� ����
            }
            if (defpos.y > transform.position.y)
                transform.position = defpos;
            //�������� �ּ� ���̴� = ������ �ִ� ��ġ��.
        
            //��� �̵�
            transform.Translate(new Vector3(-perDX, -perDY, defpos.z));
        }


        /*

        if (endX && endY)
        {
            //�̵� ����
            if (isReverse)
            {
                //��ġ�� ��߳��� ���� �����ϰ��� ���� ���� �̵����� ���ư������� �ʱ���ġ�� ������
                transform.position = defpos;
            }
            isReverse = !isReverse;
            isCanMove = false;
            if (isMoveWhenOn == false)
            {
                //�ö����� �����̴� ���� �������
                Invoke("Up", weight);
            }
        }*/



    }
    /* �� �ڵ�� ���������Ϳ��� �ö󰡰ų�, ���� ������

    public void Up()
    {
        isCanMove = true;


    }

    //�̵����� ���ϰ� �����

    public void Stop()
    {
        isCanMove = false;
    }
    */

    public void CheckPlayerZone()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, CheckRect, 0, whatIsGround);

        UpSidePlayer.Clear();//�ϴ� ���� ���� ������ ������Ʈ���� �������� ������
        int check = 0;//���� ������ CHECK�� ����
        for (int i = 0; i < colliders.Length; ++i)
        {
            var player = colliders[i].GetComponent<PlayerController>();

            if (player.isGround || player.isUpperPlayer)
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



    //�浹üũ�� OnCollision���� ó���� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�����Ѱ��� �÷��̾��� �̵� ����� �ڽ����� �����
            collision.transform.SetParent(transform);

           
        }
    }

    //�浹 ����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�浹���� �������� ���� �÷��̾��� �̵� ����� �ڽĿ��� ���ܽ�Ű��
            collision.transform.SetParent(null);
        }
    }

}
