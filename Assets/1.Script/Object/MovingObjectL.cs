using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectL : MonoBehaviour
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


    //������
    public bool isCanMove = false;
    public bool isSwitch = false;
    //1�����Ӵ� X �̵� ��
    float perDX;
    //1�����Ӵ� Y �̵� ��
    float perDY;
    //�ʱ� ��ġ
    Vector3 defPos;
    //��������
    [SerializeField] Vector3 arrPos;
   



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


    }


    void FixedUpdate()
    {

        if (isSwitch)
        {

            if (isCanMove)
            {
               
                //��� �̵�
                Vector3 v = new Vector3(perDX, perDY, defPos.z);
                transform.Translate(v);
                if (arrPos.x > transform.position.x)
                  isCanMove = false;
            }
            
        }
        else if(!isSwitch)
        {
            if (!isCanMove)
            {

                if (defPos.x < transform.position.x)
                    transform.position = defPos;
                //�������� �ּ� ���̴� = ������ �ִ� ��ġ��.

                //��� �̵�
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));
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
    /*
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
       // isCanMove = false;  //�̵� ���� ���� false


        if (isMoveWhenOn == false)
        {
            //�ö��� ��  �����̴� ���� ���� ���
            Invoke("Move", weight); //weight��ŭ ���� �� �ٽ� �̵�
        }
    }*/






    /*
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

    */
}


