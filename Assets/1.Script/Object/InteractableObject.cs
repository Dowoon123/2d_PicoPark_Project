using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public bool isAction;

    public abstract void OnAction();


    //public 


    protected void Update()
    {
        
    }


}
