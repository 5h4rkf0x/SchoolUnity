using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class KnifeManager : MonoBehaviour
{

    [SerializeField] KnifePatterns knifePrefab;
    [SerializeField] List<GameObject> knifes;

    [SerializeField] List<Transform> spawnPoints;

    private void Start()
    {
        SpawnKnives();
    }

    void SpawnKnives()
    {
        for (int i = 0; i < knifes.Count; i++)
        {
            Instantiate(knifes[i], spawnPoints[i].position, Quaternion.identity, transform);
        }
    }
}
