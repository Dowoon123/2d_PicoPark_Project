using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Cannon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shotPos;


    GameObject KeyBottle;

    [SerializeField] private bool isShot;

    // Start에서 코루틴을 변수?/??????

    Coroutine shotCoroutine;

    bool isEndShoot = false;

    void Start()
    {

        KeyBottle = GameObject.Find("KeyBottle");
        KeyBottle.GetComponent<KeyBottle>();
      

        if(PhotonNetwork.IsMasterClient)
        GetComponent<PhotonView>().RPC("Shot", RpcTarget.All);
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyBottle.GetComponent<KeyBottle>().Hp <= 0 && !isEndShoot)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                GetComponent<PhotonView>().RPC("StopShot", RpcTarget.All);
                isEndShoot = true;
                Destroy(transform.gameObject);
            }
        }
        else if(KeyBottle.GetComponent<KeyBottle>() == null)
            return;
      
    }

    [PunRPC]
    private void Shot()
    {

        shotCoroutine = StartCoroutine(ShotBullet());

    }

    [PunRPC]
    private void StopShot()
    {
        StopCoroutine(shotCoroutine);
    }

    IEnumerator ShotBullet()
    {
        while (true)
        {
            if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("CannonBullet", shotPos.transform.position, Quaternion.identity);
  
            yield return new WaitForSeconds(3f);
            
        }
    }
}
 