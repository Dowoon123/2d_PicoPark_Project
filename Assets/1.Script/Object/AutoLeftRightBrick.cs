using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AutoLeftRightBrick : MonoBehaviour
{
    Vector3 pos; //������ġ
   public float inputX = 5.0f; // ��(��)�� �̵������� (x)�ִ밪
    public float speed = 0.9f; // �̵��ӵ�

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = pos;
       // v.x += in * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}










    