using UnityEngine;

public class BossPatern : MonoBehaviour
{
    [SerializeField] private BossExplodeAreaManager bossExplodeAreaManager;

    [SerializeField] private float minPatternTime = 5.0f;
    [SerializeField] private float maxPatternTime = 10.0f;
    private float nextPatternTime = 10f;
    private float afterPatternTime = 0f;

    private void Awake()
    {
        RefreshPatternTime();
        bossExplodeAreaManager = GetComponentInChildren<BossExplodeAreaManager>();
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
    
    public void RefreshPatternTime()
    {
        nextPatternTime = Random.Range(minPatternTime, maxPatternTime);
    }

    private void FloorBombAttack()   // 지형 원형으로 랜덤하게 파괴 생성
    {
        bossExplodeAreaManager.SetFloorExplodeInfo();
    }
}
