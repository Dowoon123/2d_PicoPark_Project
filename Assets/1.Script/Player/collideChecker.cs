using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class collideChecker : MonoBehaviour
{
    [SerializeField] Transform obstacleChecker;
    [SerializeField] Transform playerChecker;
    [SerializeField] float PlayerCheckRadius;
    [SerializeField] Collider2D UpperCollider;
    [SerializeField] Collider2D BodyCollider;


    [SerializeField] LayerMask WhatIsUpper;
    [SerializeField] LayerMask WhatIsObstacle;
    
    PlayerController player;

    public bool isObstacle;
    public bool isPlayer;


    public GameObject obstacleObject;
    public GameObject playerObject;

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    public void Update()
    {
        isObstacle = IsObstacleDetected();
        isPlayer = IsPlayerDetected();

    }
    public virtual bool IsPlayerDetected()
    {
        // RaycastHit2D playerFind;

        //   playerFind = Physics2D.Raycast(playerChecker.transform.position, Vector2.right, 0.1f, WhatIsObstacle);

     
        var capsule = Physics2D.OverlapCapsule(playerChecker.transform.position, new Vector2(0.441907406f, 0.92f), CapsuleDirection2D.Vertical, 0,WhatIsObstacle);
        if (capsule)
        {
            playerObject = capsule.gameObject;


            return true;
        }
        else
        {
            playerObject = null;
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

    public void JumpCollider(bool b)
    {
        LayerMask mask;
        mask = b == true ? WhatIsUpper : 0;
        BodyCollider.excludeLayers = mask;

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(obstacleChecker.transform.position.x + 0.7f
            , transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(playerChecker.transform.position.x + 0.5f
            , transform.position.y));

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x 
           , transform.position.y -1f));

        
    }
}
