using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{

    public float x;
    public float y;
    public float z;
    public float rotateSpeed;
  
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
            z += rotateSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, z);
      
        
      
    }
}
