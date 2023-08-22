using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    /// <summary>
    /// �ش� ��ũ��Ʈ�� ��Ͽ� ���� �̵��� ���õ� ��ũ��Ʈ�� 
    /// X,Y�� �� �����ؼ� �̵��� �����ϸ� �Ѵ� ���� �밢�� �̵�ó�� ǥ���� ������
    /// ��, ��ũ��Ʈ ���� �ܰ��, X�� �̵��� Y�� �̵��� ������ �̵��� �������� �ʾ���.
    /// </summary>

    //x�̵� �Ÿ�
    public float moveX = 0.0f;
    //y�̵� �Ÿ�
    public float moveY = 0.0f;
    //�ð�
    public float times = 0.0f;
    //�����ð�
    public float weight = 0.0f;

    //�ö����� �����̱�
    public bool isMoveWhenOn = false;

    //������
    public bool isCanMove = true;
    //1�����Ӵ� X �̵� ��
    float perDX;
    //1�����Ӵ� Y �̵� ��
    float perDY;
    //�ʱ� ��ġ
    Vector3 defPos;
    //��������
    bool isReverse = false;



    void Start()
    {
        //�ʱ� ��ġ
        defPos = transform.position;
        //1�����ӿ� �̵��ϴ� �ð�
        float timestep = Time.fixedDeltaTime;
        //1�������� X �̵� ��
        perDX = moveX / (1.0f / timestep * times);
        //1�������� Y �̵� ��
        perDY = moveY / (1.0f / timestep * times);

        if (isMoveWhenOn)
        {
            //ó������ �������� �ʰ� �ö󰡸� �����̱� ����
            isCanMove = false;
        }
    }


    void FixedUpdate()
    {
        if (isCanMove)
        {
            //�̵���
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            if (isReverse)
            {
                //�ݴ���� �̵�
                //�̵����� ����� �̵� ��ġ�� �ʱ� ��ġ���� �۰ų�
                //�̵����� ������ �̵� ��ġ�� �ʱ� ��ġ���� ū���
                if ((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                {
                    //�̵����� +
                    endX = true; //X���� �̵� ����
                }
                if ((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f && y >= defPos.y))
                {
                    endY = true; //Y���� �̵� ����
                }
                //��� �̵�
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));

            }
            else
            {
                //�������̵�
                //�̵����� ����� �̵� ��ġ�� �ʱ� ��ġ���� ũ�ų�
                //�̵����� ������ �̵� ��ġ�� �ʱ� ��ġ���� �������
                if ((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x < defPos.x + moveX))
                {
                    endX = true; //X���� �̵� ����
                }
                if ((perDY >= 0.0f && y >= defPos.y + moveY) || (perDY < 0.0f && x < defPos.y + moveY))
                {
                    endY = true; //X���� �̵� ����
                }
                //��� �̵�
                Vector3 v = new Vector3(perDX, perDY, defPos.z);
                transform.Translate(v);
            }

            if (endX && endY)
            {
                //�̵� ����
                if (isReverse)
                {
                    //��ġ�� ��߳��� ���� �����ϰ��� ���� ���� �̵����� ���ư��� ���� 
                    //�ʱ� ��ġ�� ������
                    transform.position = defPos;
                }
                isReverse = !isReverse; //���� ������Ű��
                isCanMove = false;  //�̵� ���� ���� false
                if (isMoveWhenOn == false)
                {
                    //�ö��� ��  �����̴� ���� ���� ���
                    Invoke("Move", weight); //weight��ŭ ���� �� �ٽ� �̵�
                }
            }
        }
    }

    //�̵��ϰ� �����  
    public void Move()
    {
        isCanMove = true;
    }


    //�̵����� ���ϰ� �����
    public void Stop()
    {
        isCanMove = false;
    }

    //�浹
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //������ ���� �÷��̾��� �̵� ����� �ڽ����� �����
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                //�ö����� �����̴� ����
                isCanMove = true; //�̵��ϰ� �����
            }
        }
    }

    //�浹 ����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //������ ���� �÷��̾��� �̵� ����� �ڽĿ��� ���ܽ�Ű��
            collision.transform.SetParent(null);
        }
    }


}
