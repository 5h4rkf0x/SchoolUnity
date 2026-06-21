using UnityEngine;

public class ExplodeArea : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SphereCollider explodeArea;
    [SerializeField] private KnifePatterns knife;

    private bool isDamaged = false;

    private void Awake()
    {
        explodeArea = GetComponent<SphereCollider>();
        knife = GetComponentInParent<KnifePatterns>();
    }
    private void OnTriggerEnter(Collider other)
    {
        isExplodePlayer(other);
    }
    private void OnTriggerStay(Collider other)
    {
        isExplodePlayer(other);
    }

    private void isExplodePlayer(Collider other)
    {
        if (other.CompareTag("Player") && !isDamaged)
        {
            isDamaged = true;
            knife.Explode();
        }
    }

    private void OnDisable()
    {
        isDamaged = false;
    }
}
