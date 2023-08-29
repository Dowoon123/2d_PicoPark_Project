using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Key : MonoBehaviour
{
    public float speed = 0.1f;
    public GameObject Target;
    // Start is called before the first frame update
    private void Start()
    {


    }
void Update()
    {
         if(Target)
        {
            var pos = Target.transform.position;
            pos.y += 1f;
            
            transform.position = Vector3.Lerp(pos, transform.position, 0.9f);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            this.Target = collision.gameObject;
        
            
                
        
    }
}


//Vector2.MoveTowards(transform.position, collision.transform.position, speed * Time.deltaTime);










// Update is called once per frame
