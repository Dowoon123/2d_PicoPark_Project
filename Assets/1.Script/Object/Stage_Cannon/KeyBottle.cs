using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBottle : MonoBehaviour
{
    public GameObject Key;

    [SerializeField] public int Hp;
   
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("Bullet"))
        {
            

            Hp--;
            if (Hp <= 0)
            {
                Instantiate(Key, transform.position, Quaternion.identity);
                Destroy(transform.gameObject);
            }
        }
    }

}
