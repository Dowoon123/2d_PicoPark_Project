using Photon.Pun;
using UnityEngine;

public class GhostController : MonoBehaviour
{
   
    //일단 중단, ghost도 플립 구현해야됨.
    //동시에, 플레이어의 방향값도 가져와야됨 

    //스테이트 머신과 컨트롤러가 필요해졌음.\
    //스테이트 머신은 두개로만 사용할 거임.
    //IDLE, TRACE 단 두개로만 사용
    //동시에 플레이어의 상태와 방향값을 모두 체크할거임.
    //플레이어의 방향값이 유령이 바라보는 방향값과 같다면, 추적할 것이고
    //같지 않아면. 추적하지 않을 거임.
    //

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
