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

    public GameObject targetMoveBlock;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    //�浹
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("��ư�� ������");
            targetMoveBlock.GetComponent<InteractableObject>().OnAction();
            targetMoveBlock.GetComponent<InteractableObject>().isAction = true;
            //MovingObject movBlock = targetMoveBlock.GetComponent<MovingObject>();
            //  movBlock.Stop();
        }

        
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        // MovingObject movBlock = targetMoveBlock.GetComponent<MovingObject>();
        //  movBlock.Move();

    }


    // Update is called once per frame
    void Update()
    {

    }
}

