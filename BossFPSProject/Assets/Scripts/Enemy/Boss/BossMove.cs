using UnityEngine;

public class BossMove : MonoBehaviour
{
    [Header("Components")]
    // 보스 자기자신 관련
    [SerializeField] private Rigidbody rb;

    // 플레이어 관련
    [SerializeField] public PlayerMove player;
    [SerializeField] private CapsuleCollider target;

    [Header("Structs")]
    // 보스의 월드 범위 밖으로 나가는거 제한
    [SerializeField] private Vector3 minLimit = new Vector3(-20f, 2f, -10f);
    [SerializeField] private Vector3 maxLimit = new Vector3(20f, 10f, 20f);

    // 보스의 이동과 플레이어 바라보기
    private Vector3 moveLocation;
    private Vector3 targetPos;
    private Vector3 targetDir;

    [Header("Variables")]

    // 일반 이동 관련
    private bool isMoving;

    [SerializeField] private float moveArea = 3f;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float minMoveTime = 1f;
    [SerializeField] private float maxMoveTime = 4f;
    private float nextMoveTime;
    private float afterMoveTime;

    // 대쉬 관련

    public Vector3 TargetPos => targetPos;
    public Vector3 TargetDir => targetDir;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        nextMoveTime = Random.Range(minMoveTime, maxMoveTime);
    }

    private void Start()
    {
        target = player.Movecoll;
    }

    void Update() // FixedUpdate에서 관리중인 Boss의 움직임 함수를, Update에서 하는 것이 더 나은가?
    {
        afterMoveTime += Time.deltaTime;
        LookPlayer(target);
    }

    void FixedUpdate()
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
            MoveBoss();
            afterMoveTime = 0f;
            nextMoveTime = Random.Range(minMoveTime, maxMoveTime);
        }
    }

    private void LookPlayer(CapsuleCollider target)
    {
        // 보스가 플레이어를 쳐다볼 수 있도록

        targetPos = target.bounds.center;
        targetPos.y += 0.23f;

        targetDir = (targetPos - transform.position).normalized;
        rb.transform.rotation = Quaternion.LookRotation(targetDir);
    }

    private void MoveBoss()
    {
        // 랜덤 구체 범위 내에서 보스가 이동함 -> 전체 범위의 지정 필요
        Debug.Log("보스가 움직인다!");
        isMoving = true;
        SetMoveLocation();

        Vector3 dir = (moveLocation - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }
    private void SetMoveLocation()
    {
        Vector3 random = Random.insideUnitSphere * moveArea;

        moveLocation = transform.position + random;

        moveLocation.x = Mathf.Clamp(moveLocation.x, minLimit.x, maxLimit.x);
        moveLocation.y = Mathf.Clamp(moveLocation.y, minLimit.y, maxLimit.y);
        moveLocation.z = Mathf.Clamp(moveLocation.z, minLimit.z, maxLimit.z);
    }

    // Patterns

    private void DashToPlayer(CapsuleCollider target)
    {

    }
    private void FloorBombAttack(CapsuleCollider target)
    {
        // 지형 원형으로 랜덤하게 파괴 생성


    }
}
