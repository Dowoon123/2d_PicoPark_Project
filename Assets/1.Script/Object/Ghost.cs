using Photon.Pun;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    //유령 오브젝트 스크립트
    //플레이어를 바라보는 수가 0일경우 플레이어들을 향해 달려올것임.
    //단, 플레이어가 1명이라고 바라볼 경우, 움직일 수 없음.
    //움직이는 모습과 움직이지 않는 모습을 구현하여 SetActive로 구현할 것임.

    //일단 중단, ghost도 플립 구현해야됨.
    //동시에, 플레이어의 방향값도 가져와야됨 

    Rigidbody2D rb;

    public GameObject idleGhost;
    public GameObject TraceGhost;

    public Transform target; // 추적할 대상
    public float moveSpeed = 3f; // 이동 속도
    public float detectionRange = 5f; // 감지 범위

    private Vector2 currentVelocity; // 현재 속도
    private bool isBeingWatched = false; // 대상을 바라보고 있는지 여부

    private void Start()
    {
        TraceGhost.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        target = gameObject.transform.Find("Player");
    }
    private void Update()
    {
        // 대상과의 거리를 계산
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 대상이 감지 범위 내에 있을 때
        if (distanceToTarget <= detectionRange)
        {
            // 대상을 바라보는 방향 계산
            Vector3 targetDirection = (target.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, targetDirection);

            // 대상을 바라보고 있으면
            if (angleToTarget <= 30f)
            {
                isBeingWatched = true;
            }
            else
            {
                isBeingWatched = false;
            }

            // 대상을 바라보고 있지 않으면 이동
            if (!isBeingWatched)
            {
                Vector2 targetDir = new Vector2(targetDirection.x, targetDirection.y);
                currentVelocity = Vector2.MoveTowards(currentVelocity, targetDir * moveSpeed, Time.deltaTime);
                transform.Translate(currentVelocity * Time.deltaTime);
            }
            else
            {
                currentVelocity = Vector2.zero;
            }
        }
        else
        {
            isBeingWatched = false;
            currentVelocity = Vector2.zero;
        }
    }
}
