using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class collideChecker : MonoBehaviour
{
    [SerializeField] Transform obstacleChecker;
    public Transform playerChecker;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] Collider2D UpperCollider;
    [SerializeField] Collider2D BodyCollider;


    [SerializeField] LayerMask WhatIsUpper;
    [SerializeField] LayerMask WhatIsObstacle;

    PlayerController player;

    public bool isObstacle;
    public bool isPlayer;


    public GameObject obstacleObject;
    public GameObject PushedPlayer;

    public List<PlayerController> UpsidePlayers = new List<PlayerController>(); 
    public List<Vector2> upPosition = new List<Vector2>();
    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();


    }

    public void Update()
    {
      //   isObstacle = IsObstacleDetected();
        isPlayer = IsFrontObject();
      //  IsUpsideDetected();

       
    }

    public void OnDrawGizmos()
    {
        var pos = transform.position;
        pos.y += 5.0f;
        Gizmos.DrawCube(pos, new Vector3(1, 10, 0));
        // Gizmos.DrawCube(transform.position + new Vector3(rectXSize, 0,0) , CheckRect);
    }
    //public virtual void IsUpsideDetected()
    //{

    //    var pos = transform.position;
    //    pos.y += 5.0f;
        
    //    var colBox = Physics2D.OverlapBoxAll(pos, new Vector2(0.8f, 10.0f), 0, WhatIsObstacle);
    //  //  Debug.Log(colBox.Length);
    //    UpsidePlayers.Clear();
    //    upPosition.Clear();
    //    if (colBox.Length > 0)
    //    {
    //       for(int i=0; i< colBox.Length; ++i)
    //        {
    //            if (colBox[i].gameObject.layer == 6)
    //            {
    //                var pc = colBox[i].gameObject.GetComponent<PlayerController>();
                     

    //                if(pc.currState is PlayerIdleState)
    //                {
    //                    if (pc != player)
    //                    {
    //                        UpsidePlayers.Add(pc);
                    
    //                        upPosition.Add(new Vector2(pc.transform.position.x - player.transform.position.x,
    //                            pc.transform.position.y - player.transform.position.y));
    //                    }
    //                }
                     

    //            }
    //        }
        
    //    }


    //}
    
    public virtual bool IsFrontObject()
    {
        // RaycastHit2D playerFind;

        //  playerFind = Physics2D.Raycast(playerChecker.transform.position, Vector2.right, 0.1f, WhatIsObstacle);


        //  var capsule = Physics2D.OverlapCapsule(playerChecker.transform.position, new Vector2(0.441907406f, 0.92f), CapsuleDirection2D.Vertical, 0,WhatIsObstacle);

        var colBox = Physics2D.OverlapBox(playerChecker.transform.position,new Vector2(0.15f, 0.80f), 0, WhatIsObstacle);

        if (colBox)
        {
            if (colBox.gameObject.layer == 6)
                PushedPlayer = colBox.gameObject;
            else if (colBox.gameObject.layer == 9)
                obstacleObject = colBox.gameObject;
            
         


            return true;
        }
        else
        {
            PushedPlayer = null;
            obstacleObject = null;
            return false;
        }
    }
    public virtual bool IsObstacleDetected()
    {
        RaycastHit2D result;

        result = Physics2D.Raycast(obstacleChecker.transform.position, Vector2.right, 0.5f, WhatIsObstacle);
        if (result)
        {
            obstacleObject = result.collider.gameObject;


            return true;
        }
        else
        {
            obstacleObject = null;
            return false;
        }
    }
    /// <summary>
    /// True : 머리컬라이더 무시 
    /// False : 머리컬라이더 체크 
    /// </summary>
    /// <param name="b"></param>
    public void JumpCollider(bool b)
    {
        LayerMask mask;
        mask = b == true ? WhatIsUpper : 0;
        BodyCollider.excludeLayers = mask;

    }





}
