using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        var all = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 1), 0,LayerMask.GetMask("Player"));


        if(all.Length >0)
        {
            Debug.Log("¥Í∞Ì¿÷¿Ω" + all[0].name);
        }




    }
}
