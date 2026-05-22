using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Net;
using System.Collections.Generic;

public class KnifePatterns : MonoBehaviour
{
    /// <summary>
    /// 필요한 것
    /// 1. 재설정 함수들
    /// 2. 
    /// </summary>
    
    [Header("Components")]
    // 필요한 클래스 객체 불러오기
    [SerializeField] private KnifeManager knifeManager;
    [SerializeField] private BossExplodeAreaManager bossExplodeAreaManager;
    [SerializeField] private ExplodeArea explodeArea;
    GameObject explodeObj;
    public Rigidbody rb;

    [Header("Structs")]
    // 타겟 위치 및 칼 관리 변수 -> KnifeManager로 이동

    [SerializeField] private Transform bossTransform;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private Quaternion spawnRot;

    [Header("Variables")]
    [SerializeField] public int knifeID;
    private bool explosiveKnife = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        knifeManager = GetComponentInParent<KnifeManager>();
        Transform temp = gameObject.transform.root;
        bossExplodeAreaManager = temp.GetComponentInChildren<BossExplodeAreaManager>();
    }

    private void Start()
    {
        explodeObj = Instantiate(explodeArea.gameObject, gameObject.transform);
        explodeObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            knifeManager.HitPlayer();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && !rb.isKinematic)
        {
            if (explosiveKnife) return;

            knifeManager.ResetKnife(knifeID);
            knifeManager.StartReloadKnife(knifeID);
            gameObject.SetActive(false);
        }
    }

    // 칼날 초기화 관련 함수

    // 패턴 준비 함수
    private void TargetDir(Vector3 dir)
    {
        // 칼날의 방향 전환 - 타겟 방향으로
        transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(-90, 0, 0);
    }

    // 패턴 실행 함수
    public void ThrowKnife(Vector3 dir)
    {
        // 타겟에게 직접 날아가 공격 - 회전하면서 날아가는것도 좋을듯?
        rb.isKinematic = false;
        transform.SetParent(null);

        TargetDir(dir);

        rb.linearVelocity = dir * knifeManager.KnifeSpeed;
        rb.angularVelocity = transform.forward * knifeManager.RotationSpeed;
    }

    public void BombAttack(Vector3 startPos, Vector3 targetPos, Vector3 dir)
    {
        // 타겟에게 날아가 1초 정도 뒤에 폭발 ----- 시전 전의 타겟의 위치에 도달시 정지 후 폭발
        rb.isKinematic = false;
        transform.SetParent(null);
        explosiveKnife = true;

        TargetDir(dir);

        rb.linearVelocity = dir * knifeManager.KnifeSpeed;
        StartCoroutine(Explode(startPos, targetPos));
    }

    private IEnumerator Explode(Vector3 startPos, Vector3 targetPos)
    {
        float timer = 0;
        float lifeTime = 3;
        while (timer <= lifeTime)
        {
            timer += Time.deltaTime;

            if (Vector3.Distance(startPos, targetPos) <= knifeManager.KnifeSpeed * timer)
            {
                rb.linearVelocity = Vector3.zero;
                explodeObj.SetActive(false);
                yield return new WaitForSeconds(1f);
                explodeObj.SetActive(true);
                bossExplodeAreaManager.ExplodeSound();
                yield return new WaitForSeconds(1f);
                explodeObj.SetActive(false);
                explosiveKnife = false;
                knifeManager.ResetKnife(knifeID);
                knifeManager.StartReloadKnife(knifeID);
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    public void Explode()
    {
        Debug.Log("폭발 데미지 받음!!!@@@@@");
        knifeManager.ExplodePlayer();
    }
}