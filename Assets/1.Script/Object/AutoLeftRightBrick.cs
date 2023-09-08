using Photon.Pun;
using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class AutoLeftRightBrick : MonoBehaviourPunCallbacks
{
    Vector3 goalPos;
    Vector3 originPos;
    Vector3 pos; //������ġ
    public float inputX = 5.0f; // ��(��)�� �̵������� (x)�ִ밪
    public float speed = 0.9f; // �̵��ӵ�

    bool isRight = true;


    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        goalPos = originPos;
        goalPos.x += inputX;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            var dist = Vector2.Distance(goalPos, transform.position);
            if (dist <= 0.5f)
                isRight = false;

        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            var dist = Vector2.Distance(originPos, transform.position);
            if (dist <= 0.5f)
                isRight = true;
        }
    }
}










    