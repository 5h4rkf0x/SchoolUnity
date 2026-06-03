using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    // 보스 이동은 잘 되는데 이후 바로 즉사함

    [SerializeField] BossPatern bossPatern;
    [SerializeField] private List<GameObject> plusCubeList;
    [SerializeField] private List<GameObject> minusCubeList;

    [SerializeField] private Transform plusCubeTrans;
    [SerializeField] private Transform minusCubeTrans;

    [SerializeField] private GameObject plusCube;
    [SerializeField] private GameObject minusCube;

    private float summonCubeTime = 1.5f;
    [SerializeField] private float speed = 5f;
    private Vector3 dir = Vector3.zero;

    public float cubeSpeed => speed;
    public Vector3 cubeDir => dir;

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
    public void GetDirectionToTarget(Transform target, Transform startPos)
    {
        Vector3 direction = target.position - startPos.position;
        dir.x = direction.x;
        dir.y = 3f;
        dir.z = direction.z;

        dir = dir.normalized;
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

        for (int i = 0; i < 10; i++)
        {
            summonCubeTime -= 0.1f;
            int cubeStatesNum = Random.Range(0, 2);
            if (cubeStatesNum == 0)
            {
                plusCubeList[i].gameObject.transform.position = GetRandomPointOnCircleXZ(bossPatern.gameObject.transform.position, bossPatern.CubePatternRadius);
                GetDirectionToTarget(gameObject.transform, plusCubeList[i].gameObject.transform);
                plusCubeList[i].gameObject.SetActive(true);
            }
            else if (cubeStatesNum == 1)
            {
                minusCubeList[i].gameObject.transform.position = GetRandomPointOnCircleXZ(bossPatern.gameObject.transform.position, bossPatern.CubePatternRadius);
                GetDirectionToTarget(gameObject.transform, minusCubeList[i].gameObject.transform);
                minusCubeList[i].gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(summonCubeTime);
        }
    }
}
