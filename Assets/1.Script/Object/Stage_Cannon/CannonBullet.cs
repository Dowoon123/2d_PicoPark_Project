using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    public float MoveSpeed;



    GameObject KeyBottle;
   
   
    // Start is called before the first frame update
    void Start()
    {
     
        KeyBottle = GameObject.Find("KeyBottle");
        KeyBottle.GetComponent<KeyBottle>();  
    }

    // Update is called once per frame
    void Update()
    {
     
        if (KeyBottle.GetComponent<KeyBottle>().Hp == 3)
        {
            transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        }else if(KeyBottle.GetComponent<KeyBottle>().Hp == 2)
        {
            transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed * 1.5f);
        }else if(KeyBottle.GetComponent<KeyBottle>().Hp == 1) 
        {
            transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed * 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(transform.gameObject);
            KeyBottle.GetComponent<KeyBottle>().Hp = 3;
        }
        else if(collision.gameObject.CompareTag("KeyBottle"))
        {
            
            //Destroy(collision.gameObject);
            Destroy(transform.gameObject);
            
        }
    }
}
