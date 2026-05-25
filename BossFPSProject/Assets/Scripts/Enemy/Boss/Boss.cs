using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Boss: MonoBehaviour
{
    [Header("Components")]

    // 보스 관련
    [SerializeField] public Rigidbody rb;
    [SerializeField] private BossHpBar hpUI;

    // 보스 칼날 관련
    [SerializeField] KnifeManager knifeManager;
    [SerializeField] private List<Transform> knifeSpawnPoint;

    // 플레이어 관련
    [SerializeField] public PlayerMove playerMove;
    [SerializeField] public Player player;
    [SerializeField] private CapsuleCollider target;

    [Header("Structs")]
    private Vector3 targetPos;
    [SerializeField] private List<Vector3> knifeDir;

    [Header("Variables")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public List<Transform> KnifeSpawnPoint => knifeSpawnPoint;

    public CapsuleCollider Target => target;
    public Vector3 TargetPos => targetPos;
    public List<Vector3> KnifeDir => knifeDir;

    public float Health => health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        hpUI = FindFirstObjectByType<BossHpBar>();
        health = maxHealth;
        knifeManager.SpawnKnives(KnifeSpawnPoint);
    }

    private void Start()
    {
        target = playerMove.Movecoll;
        hpUI.SetHP(health, maxHealth);
    }

    private void Update()
    {
        RefreshTargetPos();
        RefreshKnifeDir(); // -----얘는 KnifeManager에서 관리하는게 메모리적으로 이득이 될 것 같은 느낌이 든다... 항상 리스트의 모든 변수를 초기화 해주지 않아도 되기 때문

        // KnifeManager에서 칼 번호를 매개변수로 받아서 if구문으로 각 번호의 칼의 방향만 바꿀까? update로 항상 리스트를 계속 초기화해주면 메모리 많이 먹을 것 같은데

    }

    private void RefreshTargetPos()
    {
        targetPos = target.bounds.center;
        targetPos.y += 0.23f;
    }
    
    private void RefreshKnifeDir()
    {
        for (int i = 0; i < 6; i++)
        {
            knifeDir[i] = (targetPos - knifeSpawnPoint[i].position).normalized;
        }
    }


    public void TakeDamage(float bulletDamage)
    {
        health -= bulletDamage;
        hpUI.SetHP(health, maxHealth);

        Debug.Log(health);

        if (health <= 0)
        {
            Die();
            hpUI.CloseBossBar();
        }
    }
    
    private void Die()
    {
        Debug.Log("보스 사망!@@@");
        gameObject.SetActive(false);
    }
}
