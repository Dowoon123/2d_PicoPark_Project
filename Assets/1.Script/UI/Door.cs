using UnityEngine;

public class Door : MonoBehaviour
{

    // 스프라이트를 변경하는 방식 오브젝트는 냅두고

    //planePlayer때문에 추가
    FlyObject planePlayer;

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
            collision.gameObject.GetComponent<PlayerController>().isNearDoor = false; // isNearDoor 는 플레이어가 문근처에있는지 체크하는 변수 이게 true일때 방향키 입력시 클리어 가능. 
            collision.gameObject.GetComponent<FlyObject>().isNearDoor = false; //planePlayer 때문에 추가
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
            if(isOpen)
            collision.gameObject.GetComponent<PlayerController>().isNearDoor = true;
            collision.gameObject.GetComponent<FlyObject>().isNearDoor = true; //planePlayer 때문에 추가
        }


    }



 }



