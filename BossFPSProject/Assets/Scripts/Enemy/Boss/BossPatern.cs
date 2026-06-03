using UnityEngine;
using System.Collections.Generic;

public class BossPatern : MonoBehaviour
{
    // 보스 이동은 잘 되는데 이후 바로 즉사함
    
    [SerializeField] private BossExplodeAreaManager bossExplodeAreaManager;
    [SerializeField] private Boss bossScript;
    [SerializeField] private BossMove bossMoveScript;

    [SerializeField] private CubeManager cubeManager;

    [SerializeField] private float cubePatternRadius;

    [SerializeField] private float minPatternTime = 5.0f;
    [SerializeField] private float maxPatternTime = 10.0f;
    private float nextPatternTime = 10f;
    private float afterPatternTime = 0f;

    [SerializeField] private List<bool> patternStates; // 0:333체력 / 1:666체력 / 2:패턴이 실행 중인가?

    public float CubePatternRadius => cubePatternRadius;

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

        // 전극패턴

        if (bossScript.Health >= 333 && bossScript.Health <= 666 && !patternStates[1])
        {
            patternStates[0] = true;
            patternStates[2] = true;
            ReadyStatePattern();
            StartCoroutine(cubeManager.UseStatePattern());
        }
        else if (bossScript.Health <= 333 && !patternStates[0])
        {
            patternStates[1] = true;
            patternStates[2] = true;
            ReadyStatePattern();
            StartCoroutine(cubeManager.UseStatePattern());
        }
    }


    // 전극패턴

    public void ReadyStatePattern()
    {
        bossScript.enabled = false;
        bossMoveScript.enabled = false;
        gameObject.transform.position = new Vector3(0, 3, 0);
    }

    public void EndSetStatePattern()
    {
        bossScript.enabled = true;
        bossMoveScript.enabled = true;
    }



    // 아아아아아아아아앜

    public void RefreshPatternTime()
    {
        nextPatternTime = Random.Range(minPatternTime, maxPatternTime);
    }

    private void FloorBombAttack()   // 지형 원형으로 랜덤하게 파괴 생성
    {
        bossExplodeAreaManager.SetFloorExplodeInfo();
    }
}
