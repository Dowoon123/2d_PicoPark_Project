using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    public GameObject closedoor;
    public GameObject opendoor;
    // Start is called before the first frame update
    void Start()
    {
        this.opendoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))

        {
            this.closedoor.SetActive(false);
            this.opendoor.SetActive(true);
        }
    }
   



}
