using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject target;
    Vector2 dir;
    Vector2 dirNo;
    // Start is called before the first frame update
    void Start()
    {
        {
            void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.tag == "Player")
                {
                    target = GameObject.Find(collision.tag);
                    dir = target.transform.position - transform.position;
                    dirNo = dir.normalized;
                  
                }


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dirNo * speed * Time.deltaTime);
    }
}
