using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBolck : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float time;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
        rb.bodyType = RigidbodyType2D.Kinematic;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0.5f)
            rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            time += Time.deltaTime;
          
        }
    }
}
