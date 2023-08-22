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

    //x�� �̵��Ÿ�
    public float moveX = 0.0f;
    //y�� �̵��Ÿ�
    public float moveY = 0.0f;
    //�ð�
    public float times = 0.0f;
    //�����ð�
    public float weight = 0.0f;

    //�ö����� �����̱� ��������.
    public bool isMoveWhenOn = false; //���ǿ� �¾�����, 

    [SerializeField] private bool isCheckIntake = false;
    //�����ο� ������ �����ϴ��� üũ�ϴ� bool��

    //�������� �����ϴ� �Ұ� üũ
    public bool isCanMove = true;

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


    private void FixedUpdate()
    {

        Debug.Log("Player üũ : " + CheckedIntake);

        if ((intake - CheckedIntake) == 0)
        {
            isCheckIntake = true;
            
        }
        else
            isCheckIntake = false;//������Ʈ ������ �� ������ �׻� ������ �������� ��� üũ�Ұ���.

        Debug.Log("isCheckIntake üũ : " + isCheckIntake);

        //������� ��������.

        if (isCanMove)
        {
            //�̵���
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;

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
                //��� �̵�
                transform.Translate(new Vector3(-perDX, -perDY, defpos.z));
            }
            /*
            if (isReverse)
            {
                //�ݴ���� �̵�
                //�̵����� ��� �̵� ��ġ�� �ʱ� ��ġ���� �۰ų�
                //�̵����� ������ �̵� ��ġ�� �ʱ� ��ġ���� ū���
                if ((perDX >= 0.0f && x <= defpos.x) || (perDX <= 0.0f && x >= defpos.x))
                {
                    //�̵����� +
                    endX = true;//X���� �̵� ����
                }
                if ((perDY >= 0.0f && y <= defpos.y) || (perDY <= 0.0f && y >= defpos.y))
                {
                    endY = true;//Y���� �̵� ����
                }
                //��� �̵�
                transform.Translate(new Vector3(-perDX, -perDY, defpos.z));
            }
            else
            {
                //�������̵�
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

    }



    public void Up()
    {
        isCanMove = true;


    }

    //�̵����� ���ϰ� �����

    public void Stop()
    {
        isCanMove = false;
    }

    //�浹üũ�� OnCollision���� ó���� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�����Ѱ��� �÷��̾��� �̵� ����� �ڽ����� �����
            collision.transform.SetParent(transform);

            if (isMoveWhenOn)
            {
                //�ö����� �����̴� ����
                isCanMove = true; // �̵��ϰ� �����
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //    PlayerController playerController = gameObject.GetComponent<PlayerController>();
            // if(playerController.IsGroundDetected())
            CheckedIntake++;

            //To do : physics2D.overlapBoxaLL �÷��̾� ��ũ��Ʈ �������� �ֵ鸸
            // isGROUND üũ�ؼ� ����׶��� ������ �ֵ鸸ŭ ī��Ʈ 
            //Ʈ���� ENTER�� ����.
            //Ʈ����EXIT�� ����

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckedIntake--;
        }
    }
}
