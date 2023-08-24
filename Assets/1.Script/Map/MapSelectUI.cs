using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


        //�������� ���ý� �� ��ȯ ��ũ��Ʈ

public enum StageName
{
    stage1, stage2, stage3, stage4,
    stage5, stage6, stage7, stage8,
};

public class MapSelectUI : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public StageName currentName;

    public Transform buttonScale;

    Vector3 defaultScale;


    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    public void OnBtnClick()
    {
        switch (currentName)
        {
            case StageName.stage1:
                SceneManager.LoadScene("SampleScene");
                break;

            case StageName.stage2:
                break;

            case StageName.stage3:
                break;
        }
    }


    // �������� ui�� ���콺 Ŀ�� ������ ������ Ŀ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
