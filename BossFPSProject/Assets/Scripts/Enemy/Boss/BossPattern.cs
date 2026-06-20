using UnityEngine;
using System.Collections.Generic;

public class BossPattern : MonoBehaviour
{
    // 보스 이동은 잘 되는데 이후 바로 즉사함

    [SerializeField] private BossExplodeAreaManager bossExplodeAreaManager;
    [SerializeField] private Boss boss;

    [SerializeField] private CubeManager cubeManager;
    [SerializeField] private StateFieldManager stateFieldManager;

    [SerializeField] private float cubePatternRadius;

    [SerializeField] private float minPatternTime = 5.0f;
    [SerializeField] private float maxPatternTime = 10.0f;
    private float nextPatternTime = 10f;
    private float afterPatternTime = 0f;

    [SerializeField] public List<bool> patternStates; // 0:333체력 / 1:666체력 / 2:패턴이 실행 중인가?

    public float CubePatternRadius => cubePatternRadius;
    public CubeManager CubeManager => cubeManager;

    private void Awake()
    {
        RefreshPatternTime();
        bossExplodeAreaManager = GetComponentInChildren<BossExplodeAreaManager>();
        boss = GetComponent<Boss>();
    }

    private void Update()
    {
        afterPatternTime += Time.deltaTime;

        if (afterPatternTime >= nextPatternTime)
        {
            afterPatternTime = 0f;
            RefreshPatternTime();
            FloorBombAttack();
        }
    }

    // 전극패턴

    public void ReadyStatePattern()
    {
        boss.isInvincible = true;
        gameObject.transform.position = new Vector3(0, 2, 0);
    }

    public void EndSetStatePattern()
    {
        boss.isInvincible = true;
        cubeManager.isCubePatternEnd = false;
        patternStates[2] = false;
    }



    // 아아아아아아아아앜

    public void RefreshPatternTime()
    {
        nextPatternTime = Random.Range(minPatternTime, maxPatternTime);
    }

    private void FloorBombAttack()   // 지형 원형으로 랜덤하게 파괴 생성
    {
        if (boss.isInvincible) return;
        bossExplodeAreaManager.SetFloorExplodeInfo();
    }
}
