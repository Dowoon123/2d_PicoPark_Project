using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shotPos;


    GameObject KeyBottle;

    [SerializeField] private bool isShot;

    void Start()
    {

        KeyBottle = GameObject.Find("KeyBottle");
        KeyBottle.GetComponent<KeyBottle>();
        isShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShot && KeyBottle.GetComponent<KeyBottle>().Hp > 0)
        {
            Shot();
        }
        else
            return;
      
    }

    private void Shot()
    {
       
            Instantiate(bullet, shotPos.transform.position, Quaternion.identity);
            StartCoroutine(isShoted());
        

    }

    IEnumerator isShoted()
    {
        isShot = true;
        yield return new WaitForSeconds(3f);
        isShot = false;
    }
}
