using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class collideChecker : MonoBehaviour
{
    [SerializeField] Transform obstacleChecker;

    [SerializeField] Collider2D UpperCollider;
    [SerializeField] Collider2D BodyCollider;


    [SerializeField] LayerMask WhatIsUpper;
    [SerializeField] LayerMask WhatIsObstacle;
    PlayerController player;

    public bool isObstacle;
    public GameObject obstacleObject;
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
        Gizmos.DrawLine(transform.position, new Vector3(obstacleChecker.transform.position.x + 0.5f
            , transform.position.y));

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x 
           , transform.position.y -1f));
    }
}
