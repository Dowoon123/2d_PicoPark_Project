using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    public GameObject pushedObject;

    public List<PlayerController> UpsidePlayers;

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
        IsUpsideDetected();
    }


    public virtual void IsUpsideDetected()
    {

        var pos = transform.position;
        pos.y += 2.0f;
        var colBox = Physics2D.OverlapBoxAll(pos, new Vector2(0.5f, 4.0f), 0, WhatIsObstacle);
        UpsidePlayers.Clear();

        if (colBox.Length > 0)
        {
           for(int i=0; i< colBox.Length; ++i)
            {
                if (colBox[i].gameObject.layer == 6)
                {
                    var pc = colBox[i].gameObject.GetComponent<PlayerController>();
                     
                    if(pc.currState is PlayerGroundedState)
                    {
                        UpsidePlayers.Add(pc);
                    }
                     

                }
            }
        
        }


    } 
    public virtual bool IsFrontObject()
    {
        // RaycastHit2D playerFind;

        //  playerFind = Physics2D.Raycast(playerChecker.transform.position, Vector2.right, 0.1f, WhatIsObstacle);


        //  var capsule = Physics2D.OverlapCapsule(playerChecker.transform.position, new Vector2(0.441907406f, 0.92f), CapsuleDirection2D.Vertical, 0,WhatIsObstacle);

        var colBox = Physics2D.OverlapBox(playerChecker.transform.position,new Vector2(0.15f, 0.80f), 0, WhatIsObstacle);

        if (colBox)
        {
            if (colBox.gameObject.layer == 6)
                pushedObject = colBox.gameObject;
            else if (colBox.gameObject.layer == 9)
                obstacleObject = colBox.gameObject;
            
         


            return true;
        }
        else
        {
            pushedObject = null;
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
    /// True : �Ӹ��ö��̴� ���� 
    /// False : �Ӹ��ö��̴� üũ 
    /// </summary>
    /// <param name="b"></param>
    public void JumpCollider(bool b)
    {
        LayerMask mask;
        mask = b == true ? WhatIsUpper : 0;
        BodyCollider.excludeLayers = mask;

    }

    protected virtual void OnDrawGizmos()
    {
      
        Gizmos.DrawLine(transform.position, new Vector3(playerChecker.transform.position.x + 0.5f
            , transform.position.y));

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x 
           , transform.position.y -1f));

        
    }



}
