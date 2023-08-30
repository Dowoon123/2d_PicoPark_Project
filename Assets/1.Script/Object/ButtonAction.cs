using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    /// <summary>
    /// 이 스크립트는 버튼 제어로 해당스크립트에서 충돌이 일어날땐, 버튼이 눌러져있고
    /// 충돌에서 벗어나면 버튼이 바로 떼지는 스크립트임.
    /// 해당 스크립트를 통해서 벽의 통과 여부를 사용할 것임.
    /// </summary>

    public GameObject targetMoveBlock;
    public Sprite imageOn;
    public Sprite imageOff;

    public bool on = false; //스위치 상태(true: 눌린 상태 false: 눌리지 않은 상태)

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

    //충돌
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!on)
            {
                on = true;
                GetComponent<SpriteRenderer>().sprite = imageOff;
               // MovingObject movBlock = targetMoveBlock.GetComponent<MovingObject>();
              //  movBlock.Stop();
            }
          
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
