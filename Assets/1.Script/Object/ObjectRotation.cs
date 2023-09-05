using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{

    public float x;
    public float y;
    public float z;
    public float rotateSpeed;
    bool isTurnRotate;
    // Start is called before the first frame update
    void Start()
    {
        z += rotateSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, z);
        isTurnRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (z > 30 )
        {
            isTurnRotate = true;
            z -= rotateSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, z);
        }
        
      
    }
}
