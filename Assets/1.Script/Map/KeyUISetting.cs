using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUISetting : MonoBehaviour
{
 
    public void ClickSetting() //Ű ���̵� ȭ�� Ŭ��
    {
        gameObject.SetActive(true);
        Debug.Log("��ư Ŭ�� ��");
    }

    public void ClickBack() //�������� ���ư���
    {
        gameObject.SetActive(false);
        Debug.Log("���ư���");

    }
}
