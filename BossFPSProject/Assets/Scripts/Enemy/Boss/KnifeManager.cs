using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class KnifeManager : MonoBehaviour
{
    /// <summary>
    /// 나이프 넘버를 리스트로 만들고 이 리스트마다 다음패턴까지의 시간이랑 이런걸 관리 할 수 있을까? -> S.O로 관리해야할까?
    /// </summary>


    [Header("Components")]
    [SerializeField] private Boss boss;

    [SerializeField] private Player player;

    [SerializeField] private KnifePatterns knifePrefab;
    [SerializeField] private List<KnifePatterns> knifePools;

    [Header("Variables")]
    private int count = 6;
    // [SerializeField] private int index = 0;

    [SerializeField] private float knifeDamage = 20;
    [SerializeField] private float explodeDamage = 40;

    [SerializeField] private float minPatternTime = 2f;
    [SerializeField] private float maxPatternTime = 5f;
    [SerializeField] private int maxPatternNum = 3;

    private float knifeSpeed = 20f;
    private float rotateSpeed = 240f;
    [SerializeField] private float knifeReloadTime = 4;

    [SerializeField] private List<int> patternNum;
    [SerializeField] private List<float> nextPatternTime;
    [SerializeField] private List<float> afterPatternTime;

    public float KnifeSpeed => knifeSpeed;
    public float RotationSpeed => rotateSpeed;

    private void Awake()
    {
        boss = GetComponentInParent<Boss>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            SetNextPatternInfo(i);
        }
        if (boss == null)
        {
            Debug.LogWarning("boss가 할당되지 않음!!!!!");
        }
    }

    private void Update()   // KnifeManager로 이동
    {
        for (int i = 0; i < count; i++)
        {
            afterPatternTime[i] += Time.deltaTime;

            if (afterPatternTime[i] >= nextPatternTime[i])
            {
                afterPatternTime[i] = 0f;
                SetNextPatternInfo(i);
                UseKnifePattern(i, patternNum[i]);
            }
        }
    }

    public void SpawnKnives(List<Transform> knifeSpawnPoints)
    {
        for (int i = 0; i < knifeSpawnPoints.Count; i++)
        {
            KnifePatterns instance = Instantiate(knifePrefab, knifeSpawnPoints[i].position, Quaternion.identity, transform);
            knifePools.Add(instance);
            knifePools[i].knifeID = i;
            SetNextPatternInfo(i);
        }
    }

    public void ResetKnife(int knifeID)
    {
        knifePools[knifeID].rb.linearVelocity = Vector3.zero;
        knifePools[knifeID].rb.angularVelocity = Vector3.zero;
        knifePools[knifeID].transform.position = boss.KnifeSpawnPoint[knifeID].transform.position;
        knifePools[knifeID].transform.rotation = boss.KnifeSpawnPoint[knifeID].transform.rotation;
        knifePools[knifeID].rb.isKinematic = true;
    }

    public void StartReloadKnife(int knifeID)
    {
        StartCoroutine(ReloadKnife(knifeID));
    }

    public IEnumerator ReloadKnife(int knifeID)
    {
        yield return new WaitForSeconds(knifeReloadTime);
        knifePools[knifeID].gameObject.SetActive(true);
    }

    private void SetNextPatternInfo(int knifeNum)
    {
        nextPatternTime[knifeNum] = Random.Range(minPatternTime, maxPatternTime);
        patternNum[knifeNum] = Random.Range(1, maxPatternNum + 1);
    }
    private void UseKnifePattern(int knifeNum, int patternNum)
    {
        if (!CheckKnifeList(knifeNum))
        {
            Debug.Log("패턴 무시됨!");
            return;
        }
        Debug.Log("\n" + knifeNum + "번째 칼\n" + patternNum + "번 패턴");
        switch (patternNum)
        {
            case 1:             // 칼 냅다 집어 던지기
                knifePools[knifeNum].ThrowKnife(boss.KnifeDir[knifeNum]);
                break;

            case 2:             // 칼 집어 던져서 폭발
                knifePools[knifeNum].BombAttack(boss.KnifeSpawnPoint[knifeNum].position, boss.TargetPos, boss.KnifeDir[knifeNum]);
                break;

            case 3:
                knifePools[knifeNum].ThrowStone(boss.KnifeDir[knifeNum]);
                break;

            default:
                break;

        }
    }

    private bool CheckKnifeList(int knifeNum)
    {
        if (knifePools[knifeNum].gameObject.activeSelf)
        {
            return true;
        }
        return false;
    }

    public void HitPlayer()
    {
        player.TakeDamage(knifeDamage);
    }

    public void ExplodePlayer()
    {
        player.ExplodeDamage(explodeDamage);
    }
}