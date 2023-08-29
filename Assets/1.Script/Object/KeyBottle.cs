using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBottle : MonoBehaviour
{
    public GameObject Key;
   
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(Key, transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
    }

}
