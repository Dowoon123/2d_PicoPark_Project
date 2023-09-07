using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class Door : MonoBehaviour
{

    // ��������Ʈ�� �����ϴ� ��� ������Ʈ�� ���ΰ�



    PlayerController player;
    public Sprite openSprite;
    public Sprite closeSprite;
    SpriteRenderer sr;

    int count = 0;
    bool isOpen = false;

    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        sr.sprite = closeSprite;

    }

    // Update is called once per frame
    void Update()
    {
    
     }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
         
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
              
                collision.gameObject.GetComponent<PlayerController>().isNearDoor = true;


            }
            else if (collision.gameObject.GetComponent<FlyObject>() != null)
            {
               
                collision.gameObject.GetComponent<FlyObject>().isNearDoor = true; //planePlayer ������ �߰�
            }
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {

            sr.sprite = openSprite;
      
            isOpen = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (isOpen)
            {
      
                if (collision.gameObject.GetComponent<PlayerController>() != null)
                {
          
                    collision.gameObject.GetComponent<PlayerController>().isNearDoor = true;


                }
                else if (collision.gameObject.GetComponent <FlyObject >() != null)
                {
             
                    collision.gameObject.GetComponent<FlyObject>().isNearDoor = true; //planePlayer ������ �߰�
                }




            }
      
            
          

        }


    }



 }



