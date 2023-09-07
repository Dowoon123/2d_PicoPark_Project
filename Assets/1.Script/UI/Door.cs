using UnityEngine;

public class Door : MonoBehaviour
{

    // ��������Ʈ�� �����ϴ� ��� ������Ʈ�� ���ΰ�

    //planePlayer������ �߰�
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
            collision.gameObject.GetComponent<PlayerController>().isNearDoor = false; // isNearDoor �� �÷��̾ ����ó���ִ��� üũ�ϴ� ���� �̰� true�϶� ����Ű �Է½� Ŭ���� ����. 
            collision.gameObject.GetComponent<FlyObject>().isNearDoor = false; //planePlayer ������ �߰�
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
            collision.gameObject.GetComponent<FlyObject>().isNearDoor = true; //planePlayer ������ �߰�
        }


    }



 }



