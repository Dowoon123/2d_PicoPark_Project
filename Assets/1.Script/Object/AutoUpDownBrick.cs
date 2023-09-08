using Photon.Pun;
using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public enum Direction
{
    Up,
    Down,

}

public class AutoUpDownBrick : MonoBehaviourPunCallbacks
{

  //  public Direction dir = Direction.Left;
    public Vector2 originPos;
    public Vector2 goalPos;

    public float inputY = 5.0f; // ��(��)�� �̵������� (x)�ִ밪
    public float speed = 0.9f; // �̵��ӵ�

    bool isUp = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        goalPos = originPos;
         goalPos.y += inputY;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (isUp)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            var dist = Vector2.Distance(goalPos, transform.position);
            if (dist <= 0.5f)
                isUp = false;

        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);

            var dist = Vector2.Distance(originPos, transform.position);
            if (dist <= 0.5f)
                isUp = true;




        }

        //Vector3 v = pos;
        //v.y += inputY * Mathf.Sin(Time.deltaTime * speed);
        //transform.position = v;

         
    }

    
}
