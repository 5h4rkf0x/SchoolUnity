using UnityEngine;

public class ExplodeArea : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SphereCollider explodeArea;
    [SerializeField] private KnifePatterns knife;

    private void Awake()
    {
        explodeArea = GetComponent<SphereCollider>();
        knife = GetComponentInParent<KnifePatterns>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("플레이어가 폭발범위 내부입니다!!!!!");
            knife.Explode();
        }
    }
}
