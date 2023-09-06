using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AutoLeftRightBrick : MonoBehaviour
{
    Vector3 pos; //현재위치
   public float inputX = 5.0f; // 좌(우)로 이동가능한 (x)최대값
    public float speed = 0.9f; // 이동속도

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










    