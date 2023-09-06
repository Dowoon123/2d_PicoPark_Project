using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
    /// <summary>
    /// 이 스크립트는 버튼 제어로 해당스크립트에서 충돌이 일어날땐, 버튼이 눌러져있고
    /// 충돌에서 벗어나면 버튼이 바로 떼지는 스크립트임.
    /// 해당 스크립트를 통해서 벽의 통과 여부를 사용할 것임.
    /// </summary>

    public GameObject targetMoveBlock;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    //충돌
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("버튼은 눌렸음");
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

