using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBottle : MonoBehaviour
{
    public GameObject Key;
    public Sprite[] keyImage;

    SpriteRenderer image;

    [SerializeField] public int Hp;
   
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        
    }

 
    void Update()
    {
        if(Hp ==3)
        {
            image.sprite = keyImage[0];

        }else if(Hp == 2)
        {
            image.sprite = keyImage[1];
        }
        else if(Hp == 1) 
        {
            image.sprite= keyImage[2];
        }
        
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
