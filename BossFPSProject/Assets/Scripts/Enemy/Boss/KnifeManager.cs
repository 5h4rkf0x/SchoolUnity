using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class KnifeManager : MonoBehaviour
{
    /// <summary>
    /// 나이프 넘버를 리스트로 만들고 이 리스트마다 다음패턴까지의 시간이랑 이런걸 관리 할 수 있을까? -> S.O로 관리해야할까?
    /// </summary>
    

    [Header("Components")]
    [SerializeField] KnifePatterns knifePrefab;
    [SerializeField] List<GameObject> knifes;

    [SerializeField] List<Transform> spawnPoints;

    [Header("Variables")]
    private int knifeNum;

    public List<Transform> SpawnPoints => spawnPoints;

    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            knifes[i] = knifePrefab.gameObject;
        }
    }
    private void Start()
    {
        SpawnKnives();
    }

    private void SpawnKnives()
    {
        for (int i = 0; i < knifes.Count; i++)
        {
            Instantiate(knifes[i], spawnPoints[i].position, Quaternion.identity, transform);
        }
    }
}
