using UnityEngine;

public class DownBolck : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float time;
    [SerializeField] float downSpeed;
    [SerializeField] bool isStep = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isStep)
            time += Time.deltaTime;

        if (time > 0.3f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * downSpeed);
            Destroy(transform.gameObject, 2f);
        }
          
        




    }




private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        isStep = true;
    }
}





}
