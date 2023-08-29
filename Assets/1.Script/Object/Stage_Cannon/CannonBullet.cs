using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    public float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(transform.gameObject);
        }
        else if(collision.gameObject.CompareTag("KeyBottle"))
        {
            //Destroy(collision.gameObject);
            Destroy(transform.gameObject);
            
        }
    }
}
