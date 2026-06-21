using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    // 보스 이동은 잘 되는데 이후 바로 즉사함

    [SerializeField] BossPattern bossPatern;
    [SerializeField] private List<GameObject> plusCubeList;
    [SerializeField] private List<GameObject> minusCubeList;

    [SerializeField] private Transform plusCubeTrans;
    [SerializeField] private Transform minusCubeTrans;

    [SerializeField] private GameObject plusCube;
    [SerializeField] private GameObject minusCube;

    private HashSet<GameObject> activeCubes = new HashSet<GameObject>();

    [SerializeField] private float speed = 3f;

    public bool isCubePatternEnd = false;
    private int targetCubeCount = 10;
    private int spawnedCubeCount;
    private bool isSpawning;

    public float cubeSpeed => speed;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            plusCubeList.Add(Instantiate(plusCube, plusCubeTrans));
            plusCubeList[i].gameObject.SetActive(false);

            minusCubeList.Add(Instantiate(minusCube, minusCubeTrans));
            minusCubeList[i].gameObject.SetActive(false);
        }
    }

    public Vector3 GetRandomPointOnCircleXZ(Vector3 center, float radius)
    {
        float angleRad = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        float x = center.x + Mathf.Cos(angleRad) * radius;
        float z = center.z + Mathf.Sin(angleRad) * radius;

        return new Vector3(x, center.y, z);
    }

    public IEnumerator UseStatePattern()
    {
        isCubePatternEnd = false;
        isSpawning = true;
        spawnedCubeCount = 0;
        activeCubes.Clear();
        float summonCubeTime = 1.5f;

        for (int i = 0; i < targetCubeCount; i++)
        {
            summonCubeTime -= 0.05f;
            int cubeStatesNum = Random.Range(1, 201);

            GameObject cube;

            if (cubeStatesNum <= 100)
            {
                cube = plusCubeList[i];
            }
            else
            {
                cube = minusCubeList[i];
            }

            cube.transform.position = GetRandomPointOnCircleXZ(bossPatern.transform.position, bossPatern.CubePatternRadius);

            cube.SetActive(true);
            RegisterCubeSpawned(cube);

            yield return new WaitForSeconds(summonCubeTime);
        }
        isSpawning = false;
        TryEndCubePattern();
    }

    public void RegisterCubeSpawned(GameObject cube)
    {
        spawnedCubeCount++;
        activeCubes.Add(cube);
    }

    public void NotifyCubeRemoved(GameObject cube)
    {
        if (!activeCubes.Remove(cube))
            return;

        TryEndCubePattern();
    }

    private void TryEndCubePattern()
    {
        if (isSpawning) return;
        if (spawnedCubeCount < targetCubeCount) return;
        if (activeCubes.Count > 0) return;

        isCubePatternEnd = true;
    }
}
