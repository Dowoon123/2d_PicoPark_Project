using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickButton : MonoBehaviour
{
    /// <summary>
    /// �� ��ũ��Ʈ�� ��ư ����� �ش罺ũ��Ʈ���� �浹�� �Ͼ��, ��ư�� �������ְ�
    /// �浹���� ����� ��ư�� �ٷ� ������ ��ũ��Ʈ��.
    /// �ش� ��ũ��Ʈ�� ���ؼ� ���� ��� ���θ� ����� ����.
    /// </summary>

    public GameObject targetBlock;
   
    public Sprite imageOn;
    public Sprite imageOff;

    public bool on = false; //����ġ ����(true: ���� ���� false: ������ ���� ����)

    // Start is called before the first frame update
    void Start()
    {

        targetBlock.SetActive(false);
        
        on = true;
        if (on)
        {
            GetComponent<SpriteRenderer>().sprite = imageOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = imageOff;
        }

    }



    //�浹
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Obstacle")
        {
            on = false;
            GetComponent<SpriteRenderer>().sprite = imageOff;

            targetBlock.SetActive(true);
            
        }

    }

    
    void Update()
    {
        if (on)
        {
            GetComponent<SpriteRenderer>().sprite = imageOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = imageOff;
        }
    }
}
