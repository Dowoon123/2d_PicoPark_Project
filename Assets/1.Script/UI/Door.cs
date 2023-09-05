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
            collision.gameObject.GetComponentInChildren<PlayerController>().nextstage = null;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {

            sr.sprite = openSprite;
         
            isOpen = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            collision.gameObject.GetComponent<PlayerController>().nextstage = this.gameObject;
        }


    }

    public void NextStage()
    {
        if (!player.nextstage)
        {
            Debug.Log("대기");
        }
        else if (player.nextstage)
        {
            if (isOpen)
            {
                Debug.Log("스테이지클리어");
                Time.timeScale = 0;
                player.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
    }
    public void ComeBackStage()
    {
        if (!player.nextstage)
        {
            Debug.Log("대기");
        }
        else if (player.nextstage)
        {
            Debug.Log("스테이지복귀");
            Time.timeScale = 1;
            player.GetComponentInChildren<SpriteRenderer>().enabled = true;

        }

    }


}
