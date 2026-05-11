using UnityEditor;
using UnityEngine;

public class KnifePatterns : MonoBehaviour
{
    [Header("Components")]
    // 필요한 클래스 객체 불러오기
    [SerializeField] private BossPatern boss;
    [SerializeField] private CapsuleCollider target;
    private Rigidbody rb;

    [Header("Structs")]
    // 타겟 위치 및 칼 관리 변수
    private Vector3 spawnPos;
    private Quaternion spawnRot;
    private Vector3 targetPos;

    [Header("Variables")]
    [Header("짤패턴 시간 범위")]
    [SerializeField] private float minPatternTime = 2f;
    [SerializeField] private float maxPatternTime = 5f;

    // 패턴 시간 관리 변수
    private float afterPatternTime;
    private float nextPatternTime;

    // 패턴 선택 관리 변수
    private int maxPatternNum = 3;
    private int patternNum;

    private float knifeSpeed = 20f;
    private float rotateSpeed = 240f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Transform bossOverall = transform.root;
        boss = bossOverall.GetComponentInChildren<BossPatern>();

        spawnPos = transform.position;
        spawnRot = transform.rotation;

        if (boss == null) Debug.Log("MISSING!!!!");
    }

    void Start()
    {
        if (boss == null) return;

        if (boss.player == null)
        {
            Debug.Log("boss.player MISSING!!!!");
            return;
        }

        target = boss.player.Movecoll;
        SetNextPatternInfo();
    }

    void Update()
    {
        afterPatternTime += Time.deltaTime;

        if (afterPatternTime >= nextPatternTime)
        {
            afterPatternTime = 0f;
            SetNextPatternInfo();
            UseKnifePattern();
        }
    }

    private void FixedUpdate()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = spawnPos;
            transform.rotation = spawnRot;
            gameObject.SetActive(false);
        }
    }

    private void SetNextPatternInfo()
    {
        nextPatternTime = Random.Range(minPatternTime, maxPatternTime);
        patternNum = Random.Range(1, maxPatternNum + 1);
    }

    private void UseKnifePattern()
    {
        Debug.Log("짤패턴 시작");
        switch (patternNum)
        {
            case 1:             // 칼 냅다 집어 던지기
                ThrowKnife(target);
                break;

            case 2:             // 칼 집어 던져서 폭발
                BombAttack(target);
                break;

            case 3:
                ThrowStone(target);
                break;

            default:
                break;

        }
        Debug.Log("짤패턴 종료");
    }

    // 패턴 준비 함수
    private void TargetDir(CapsuleCollider target)
    {
        // 칼날의 방향 전환 - 타겟 방향으로
        transform.LookAt(target.center);
        Vector3 dir = (target.center - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(-90, 0, 0);
    }
    private void PerpendicularDir(CapsuleCollider target)
    {
        // 칼날의 방향 전환 - 타겟과 수직으로
        Vector3 dir = target.center - transform.position;
        dir.y = 0f;

        if (dir == Vector3.zero) return;

        Vector3 sideDir = Vector3.Cross(Vector3.up, dir.normalized);

        transform.rotation = Quaternion.LookRotation(sideDir);
    }

    // 패턴 실행 함수
    private void ThrowKnife(CapsuleCollider target)
    {
        // 타겟에게 직접 날아가 공격 - 회전하면서 날아가는것도 좋을듯?
        TargetDir(target);
        Vector3 dir = (target.center - transform.position).normalized;

        rb.linearVelocity = dir * knifeSpeed;
        rb.angularVelocity = transform.forward * rotateSpeed;
    }

    private void BombAttack(CapsuleCollider target)
    {
        // 타겟에게 날아가 1초 정도 뒤에 폭발
        // targetPos = target.position;
        // 
        // TargetDir(target);
    }

    private void ThrowStone(CapsuleCollider target)
    {
        // 땅으로 내려가서 돌을 가져온뒤 타겟의 수직방향에서 투석기처럼 모션하기


        // PerpendicularDir(target);


    }
}
