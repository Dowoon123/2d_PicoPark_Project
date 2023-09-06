using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AutoUpDownBrick : MonoBehaviour
{
    Vector3 pos; //������ġ
    public float inputY = 5.0f; // ��(��)�� �̵������� (x)�ִ밪
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
        v.y += inputY * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
