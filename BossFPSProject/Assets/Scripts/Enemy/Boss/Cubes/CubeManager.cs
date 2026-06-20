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

    [SerializeField] private float speed = 3f;
    public bool isCubePatternEnd = false;

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

        float summonCubeTime = 1.5f;

        for (int i = 0; i < 10; i++)
        {
            summonCubeTime -= 0.1f;
            int cubeStatesNum = Random.Range(1, 201);
            if (cubeStatesNum <= 100)
            {
                plusCubeList[i].gameObject.transform.position = GetRandomPointOnCircleXZ(bossPatern.gameObject.transform.position, bossPatern.CubePatternRadius);
                plusCubeList[i].gameObject.SetActive(true);
            }
            else if (cubeStatesNum > 100)
            {
                minusCubeList[i].gameObject.transform.position = GetRandomPointOnCircleXZ(bossPatern.gameObject.transform.position, bossPatern.CubePatternRadius);
                minusCubeList[i].gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(summonCubeTime);
        }
        isCubePatternEnd = true;
    }
}
