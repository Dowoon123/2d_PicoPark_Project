using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkTest : MonoBehaviourPunCallbacks
{
    [SerializeField] Text txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                txt.text = "R 눌림";

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                txt.text = "E 눌림";
            }
        }
    }
}
