using UnityEngine;

public class BossMove : MonoBehaviour
{
    [Header("Components")]
    // 보스 자기자신 관련
    [SerializeField] private Boss boss;

    [Header("Structs")]
    // 보스의 월드 범위 밖으로 나가는거 제한
    [SerializeField] private Vector3 minLimit = new Vector3(-20f, 2f, -10f);
    [SerializeField] private Vector3 maxLimit = new Vector3(20f, 10f, 20f);

    // 보스의 이동과 플레이어 바라보기
    private Vector3 moveLocation;

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

    void Awake()
    {
        nextMoveTime = Random.Range(minMoveTime, maxMoveTime);
    }

    void Update() // FixedUpdate에서 관리중인 Boss의 움직임 함수를, Update에서 하는 것이 더 나은가?
    {
        if (!CanMove()) return;
        afterMoveTime += Time.deltaTime;
        LookPlayer(boss.TargetPos);
    }

    void FixedUpdate()
    {
        if (!CanMove())
        {
            StopMove();
            return;
        }

        if (isMoving)
        {
            float distance = Vector3.Distance(transform.position, moveLocation);

            if (distance <= 0.2f)
            {
                boss.rb.linearVelocity = Vector3.zero;
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

    private void LookPlayer(Vector3 targetPos)
    {
        // 보스가 플레이어를 쳐다볼 수 있도록

        Vector3 targetDir = (targetPos - transform.position).normalized;
        boss.rb.transform.rotation = Quaternion.LookRotation(targetDir);
    }

    private void SetMoveLocation()
    {
        Vector3 random = Random.insideUnitSphere * moveArea;

        moveLocation = transform.position + random;

        moveLocation.x = Mathf.Clamp(moveLocation.x, minLimit.x, maxLimit.x);
        moveLocation.y = Mathf.Clamp(moveLocation.y, minLimit.y, maxLimit.y);
        moveLocation.z = Mathf.Clamp(moveLocation.z, minLimit.z, maxLimit.z);
    }

    private bool CanMove()
    {
        return !boss.isInvincible && !boss.BossPattern.patternStates[2];
    }

    public void StopMove()
    {
        isMoving = false;
        boss.rb.linearVelocity = Vector3.zero;
        boss.rb.angularVelocity = Vector3.zero;
    }

    private void MoveBoss()
    {
        if (boss.isInvincible) return;
        // 랜덤 구체 범위 내에서 보스가 이동함 -> 전체 범위의 지정 필요
        Debug.Log("보스가 움직인다!");
        isMoving = true;
        SetMoveLocation();

        Vector3 dir = (moveLocation - transform.position).normalized;
        boss.rb.linearVelocity = dir * moveSpeed;
    }
}
