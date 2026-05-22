using UnityEngine;

public class BossExplodeArea : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SphereCollider bossExplodeArea;
    [SerializeField] private BossExplodeAreaManager bossExplodeAreaManager;

    private void Awake()
    {
        bossExplodeArea = GetComponent<SphereCollider>();
        bossExplodeAreaManager = FindFirstObjectByType<BossExplodeAreaManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("플레이어가 폭발범위 내부입니다!!!!!");
            bossExplodeAreaManager.ExplodePlayer();
        }
    }
}
