using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    public GameObject door;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (door.tag == "Player")
        {
          
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))

            if (player ==Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("¹®");
                NextStageReady();
            }
        this.door.SetActive(false);
    }
    private void NextStageReady()
    {
          // Destroy(gameObject);
    }




}
