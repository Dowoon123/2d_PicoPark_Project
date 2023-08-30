using UnityEngine;

public class DownBolck : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float time;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Static;
        rb.gravityScale = 0;

    }

    // Update is called once per frame
    void Update()
    {


        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            time += Time.deltaTime;
        }
        else if (time > 1f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * time);
          
        }




    }




private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        time = 0;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}





}
