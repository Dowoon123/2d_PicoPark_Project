using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


        //스테이지 선택시 씬 전환 스크립트

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


    // 스테이지 ui에 마우스 커서 닿으면 스케일 커짐
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
