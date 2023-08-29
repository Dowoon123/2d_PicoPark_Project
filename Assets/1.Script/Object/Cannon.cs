using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shotPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bullet == null) 
        {
            Instantiate(bullet, shotPos.transform.position, Quaternion.identity);
        
        }
    }
}
