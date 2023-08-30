using Photon.Pun;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    //���� ������Ʈ ��ũ��Ʈ
    //�÷��̾ �ٶ󺸴� ���� 0�ϰ�� �÷��̾���� ���� �޷��ð���.
    //��, �÷��̾ 1���̶�� �ٶ� ���, ������ �� ����.
    //�����̴� ����� �������� �ʴ� ����� �����Ͽ� SetActive�� ������ ����.

    //�ϴ� �ߴ�, ghost�� �ø� �����ؾߵ�.
    //���ÿ�, �÷��̾��� ���Ⱚ�� �����;ߵ� 

    Rigidbody2D rb;

    public GameObject idleGhost;
    public GameObject TraceGhost;

    public Transform target; // ������ ���
    public float moveSpeed = 3f; // �̵� �ӵ�
    public float detectionRange = 5f; // ���� ����

    private Vector2 currentVelocity; // ���� �ӵ�
    private bool isBeingWatched = false; // ����� �ٶ󺸰� �ִ��� ����

    private void Start()
    {
        TraceGhost.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        target = gameObject.transform.Find("Player");
    }
    private void Update()
    {
        // ������ �Ÿ��� ���
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // ����� ���� ���� ���� ���� ��
        if (distanceToTarget <= detectionRange)
        {
            // ����� �ٶ󺸴� ���� ���
            Vector3 targetDirection = (target.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, targetDirection);

            // ����� �ٶ󺸰� ������
            if (angleToTarget <= 30f)
            {
                isBeingWatched = true;
            }
            else
            {
                isBeingWatched = false;
            }

            // ����� �ٶ󺸰� ���� ������ �̵�
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
