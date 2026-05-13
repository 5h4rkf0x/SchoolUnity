using UnityEngine;

public class BossMove : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] public PlayerMove player;
    [SerializeField] private CapsuleCollider target;

    [Header("Structs")]
    private Vector3 moveLocation;
    private Vector3 targetPos;

    [Header("Variables")]
    private bool isMoving;

    [SerializeField] private float moveArea = 3f;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float minMoveTime = 1f;
    [SerializeField] private float maxMoveTime = 4f;
    private float nextMoveTime;
    private float afterMoveTime;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        player = FindFirstObjectByType<PlayerMove>();
        nextMoveTime = Random.Range(minMoveTime, maxMoveTime);
        target = player.Movecoll;
    }

    void Update() // FixedUpdate에서 관리중인 Boss의 움직임 함수를, Update에서 하는 것이 더 나은가?
    {
        afterMoveTime += Time.deltaTime;
        TargetDir(target);
    }

    void FixedUpdate() // 지금처럼 전역변수로 다루는게 나은가? 아니라면 vector3형태의 moveLocation을 fixedUpdate에서 생성하는게 이득인가?
    {
        if (isMoving)
        {
            float distance = Vector3.Distance(transform.position, moveLocation);

            if (distance <= 0.2f)
            {
                rb.linearVelocity = Vector3.zero;
                isMoving = false;
            }
        }

        if (afterMoveTime >= nextMoveTime && !isMoving)
        {
            afterMoveTime = 0f;
            MoveBoss();
            nextMoveTime = Random.Range(minMoveTime, maxMoveTime);
        }
    }

    private void TargetDir(CapsuleCollider target)
    {
        // 칼날의 방향 전환 - 타겟 방향으로
        targetPos = target.bounds.center;
        targetPos.y += 0.23f;

        Vector3 dir = (targetPos - transform.position).normalized;
        rb.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void MoveBoss()
    {
        Debug.Log("보스가 움직인다!"); // 실행되는 중

        isMoving = true;

        Vector3 random = Random.insideUnitSphere * moveArea;
        moveLocation = transform.position + random;

        Vector3 dir = (moveLocation - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }
}
