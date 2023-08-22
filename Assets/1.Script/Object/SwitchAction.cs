using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    /// <summary>
    /// �� ��ũ��Ʈ�� ���� ��ũ��Ʈ�� ������ On �ٽ� ������ Off��.
    /// </summary>

    public GameObject targetMoveBlock;
    public Sprite imageOn;
    public Sprite imageOff;
    public bool on = false; //����ġ ����(true: ���� ���� false: ������ ���� ����)


    // Start is called before the first frame update
    void Start()
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

    //�浹
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (on)
            {
                on = false;
                GetComponent<SpriteRenderer>().sprite = imageOff;
                MovingObject movBlock = targetMoveBlock.GetComponent<MovingObject>();
                movBlock.Stop();
            }
            else
            {
                on = true;
                GetComponent<SpriteRenderer>().sprite = imageOn;
                MovingObject movBlock = targetMoveBlock.GetComponent<MovingObject>();
                movBlock.Move();
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
