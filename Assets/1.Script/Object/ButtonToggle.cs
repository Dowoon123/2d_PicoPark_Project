using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
    /// <summary>
    /// �� ��ũ��Ʈ�� ��ư ����� �ش罺ũ��Ʈ���� �浹�� �Ͼ��, ��ư�� �������ְ�
    /// �浹���� ����� ��ư�� �ٷ� ������ ��ũ��Ʈ��.
    /// �ش� ��ũ��Ʈ�� ���ؼ� ���� ��� ���θ� ����� ����.
    /// </summary>

    public GameObject Interaction;
    public Sprite imageOn;
    public Sprite imageOff;

    public bool on = false; //����ġ ����(true: ���� ���� false: ������ ���� ����)

    // Start is called before the first frame update
    void Start()
    {
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
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
       
                GetComponent<SpriteRenderer>().sprite = imageOff;

                   Interaction.GetComponent<InteractableObject>().OnAction();
                // MovingObject movBlock = targetMoveBlock.GetComponent<MovingObject>();
                //  movBlock.Stop();
            
          
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
      
            on = false;
            GetComponent<SpriteRenderer>().sprite = imageOn;

          
         

           // MovingObject movBlock = targetMoveBlock.GetComponent<MovingObject>();
          //  movBlock.Move();
        
    }


    // Update is called once per frame
    void Update()
    {

    }
}
