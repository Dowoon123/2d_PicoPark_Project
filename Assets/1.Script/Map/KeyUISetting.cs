using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUISetting : MonoBehaviour
{
 
    public void ClickSetting() //키 가이드 화면 클릭
    {
        gameObject.SetActive(true);
        Debug.Log("버튼 클릭 함");
    }

    public void ClickBack() //게임으로 돌아가기
    {
        gameObject.SetActive(false);
        Debug.Log("돌아가기");

    }
}
