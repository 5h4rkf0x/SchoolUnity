using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BossExplodeAreaManager : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private GameObject tempLocation;

    // 주의표시
    [SerializeField] private GameObject Warning;
    [SerializeField] public List<GameObject> bossExplodeWarningPools;

    // 실제 폭발
    [SerializeField] private BossExplodeArea bossExplodeArea;
    [SerializeField] private List<BossExplodeArea> explodeAreaPools;

    // 폭발 효과음
    [SerializeField] private AudioClip explodeClip;

    [SerializeField] private Vector2 minAreaPoint;
    [SerializeField] private Vector2 maxAreaPoint;

    [SerializeField] private int count = 10;
    [SerializeField] private float explodeDamage = 30f;

    private void Awake()
    {
        boss = GetComponentInParent<Boss>();
        tempLocation = GameObject.Find("Temp");

        for (int i = 0; i < count; i++)
        {
            BossExplodeArea tempExplodeAreas = Instantiate(bossExplodeArea, Vector3.zero, Quaternion.identity, tempLocation.transform);
            explodeAreaPools.Add(tempExplodeAreas);
            explodeAreaPools[i].gameObject.SetActive(false);

            GameObject tempWarningPools = Instantiate(Warning, Vector3.zero, Quaternion.identity, tempLocation.transform);
            bossExplodeWarningPools.Add(tempWarningPools);
            bossExplodeWarningPools[i].gameObject.SetActive(false);
        }
    }

    public void ExplodePlayer()
    {
        boss.player.ExplodeDamage(explodeDamage);
    }

    public void SetFloorExplodeInfo()
    {
        Vector3 tempExplodeArea;
        tempExplodeArea.y = 0.03f;

        for (int i = 0; i < count; i++)
        {
            tempExplodeArea.x = Random.Range(minAreaPoint.x, maxAreaPoint.x);
            tempExplodeArea.z = Random.Range(minAreaPoint.y, maxAreaPoint.y);

            explodeAreaPools[i].transform.position = tempExplodeArea;
            bossExplodeWarningPools[i].transform.position = tempExplodeArea;
        }
        StartCoroutine(StartExplodePattern());
    }

    private IEnumerator StartExplodePattern()
    {
        Debug.Log("폭발패턴 실행");
        for (int i = 0; i < count; i++)
        {
            bossExplodeWarningPools[i].SetActive(true);
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < count; i++)
        {
            bossExplodeWarningPools[i].SetActive(false);
            explodeAreaPools[i].gameObject.SetActive(true);
        }
        ExplodeSound();
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < count; i++)
        {
            explodeAreaPools[i].gameObject.SetActive(false);
        }
        yield break;
    }

    public void ExplodeSound()
    {
        AudioManager.instance.PlayExplode(explodeClip);
    }
}
