using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActionR : MonoBehaviour
{
    /// <summary>
    /// 이 스크립트는 레버 스크립트로 누르면 On 다시 누르면 Off임.
    /// </summary>

    public GameObject targetMoveBlock;
    public Sprite imageOn;
    public Sprite imageOff;
    public bool on = false; //스위치 상태(true: 눌린 상태 false: 눌리지 않은 상태)

    Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    //충돌
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Obstacle"))
        {
            on = true;
            GetComponent<SpriteRenderer>().sprite = imageOn;
            MovingObjectR movBlock = targetMoveBlock.GetComponent<MovingObjectR>();
            movBlock.isSwitch = true;
            
            movBlock.Move();


        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {

      

        on = false;
        GetComponent<SpriteRenderer>().sprite = imageOff;
        MovingObjectR movBlock = targetMoveBlock.GetComponent<MovingObjectR>();
        movBlock.isSwitch = false;
        movBlock.Stop();

        // MovingObject movBlock = targetMoveBlock.GetComponent<MovingObject>();
        //  movBlock.Move();

    }



    // Update is called once per frame
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
