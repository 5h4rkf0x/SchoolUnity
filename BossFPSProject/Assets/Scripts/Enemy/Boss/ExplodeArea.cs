using UnityEngine;

public class ExplodeArea : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SphereCollider explodeArea;
    [SerializeField] private KnifeManager knifeManager;

    private void Awake()
    {
        knifeManager = GetComponentInParent<KnifeManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("플레이어가 폭발범위 내부입니다!!!!!");
            knifeManager.ExplodePlayer();
        }
    }
}
