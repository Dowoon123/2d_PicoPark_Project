using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shotPos;

    [SerializeField] private bool isShot;

    void Start()
    {
        isShot = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(!isShot)
        {
           Shot();
        }
      
    }

    private void Shot()
    {
        Instantiate(bullet, shotPos.transform.position, Quaternion.identity);
        StartCoroutine(isShoted());

    }

    IEnumerator isShoted()
    {
        isShot = true;
        yield return new WaitForSeconds(5f);
        isShot = false;
    }
}
