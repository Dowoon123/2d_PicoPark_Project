using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class Door : MonoBehaviour
{

    // 스프라이트를 변경하는 방식 오브젝트는 냅두고



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
            Debug.Log("이즈오픈");
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                Debug.Log("플레이어");
                collision.gameObject.GetComponent<PlayerController>().isNearDoor = true;


            }
            else if (collision.gameObject.GetComponent<FlyObject>() != null)
            {
                Debug.Log("플레인");
                collision.gameObject.GetComponent<FlyObject>().isNearDoor = true; //planePlayer 때문에 추가
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
                Debug.Log("이즈오픈");
                if (collision.gameObject.GetComponent<PlayerController>() != null)
                {
                    Debug.Log("플레이어");
                    collision.gameObject.GetComponent<PlayerController>().isNearDoor = true;


                }
                else if (collision.gameObject.GetComponent <FlyObject >() != null)
                {
                    Debug.Log("플레인");
                    collision.gameObject.GetComponent<FlyObject>().isNearDoor = true; //planePlayer 때문에 추가
                }




            }
      
            
          

        }


    }



 }



