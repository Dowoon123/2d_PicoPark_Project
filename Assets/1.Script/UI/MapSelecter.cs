using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapSelecter : MonoBehaviourPunCallbacks
{
    public Map[] Maps;
    public PhotonView pv;
    public int currentIndex = 0;

    public GameObject SelectBox;

    public Text MapTitle;
    public Text MapSubTitle;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        StartCoroutine(ReSizeBox());
        
    }

    public void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentIndex++;
                if (currentIndex > Maps.Length - 1)
                    currentIndex = 0;

                pv.RPC("ChangeBox", RpcTarget.All,currentIndex);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                currentIndex--;
                if (currentIndex < 0)
                    currentIndex = Maps.Length - 1;


                pv.RPC("ChangeBox", RpcTarget.All, currentIndex);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentIndex += 2;

                if (currentIndex > 3)
                {
                    currentIndex -= Maps.Length;
                }

                pv.RPC("ChangeBox", RpcTarget.All, currentIndex);

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {

                currentIndex -= 2;

                if (currentIndex < 0)
                {
                    currentIndex += Maps.Length;
                }

                pv.RPC("ChangeBox", RpcTarget.All, currentIndex);
            }



            if(Input.GetKeyDown(KeyCode.E)  || Input.GetKeyDown(KeyCode.Space))
            {
                PhotonNetwork.LoadLevel(Maps[currentIndex].Scene_name);
            }
        }
        
    }



    [PunRPC]
    public void ChangeBox(int curr)
    {
        SelectBox.transform.position = Maps[curr].transform.position;

        MapTitle.text = Maps[curr].GetComponent<Map>().Map_name;
        MapSubTitle.text = Maps[curr].GetComponent<Map>().Map_subTitle;
    }


    IEnumerator ReSizeBox()
    {
        bool isSmall = false;
        while(true)
        {
            if(!isSmall)
            {
                SelectBox.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                
                if(SelectBox.transform.localScale.x <= 0.9f)
                {
                    isSmall = true;
                }

                yield return new WaitForSeconds(0.05f);

            }

            if(isSmall)
            {
                SelectBox.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);

                if (SelectBox.transform.localScale.x >= 1.1f)
                {
                    isSmall = false;
                }

                yield return new WaitForSeconds(0.05f);
            }
            


        }
     
    }
    



}
