using UnityEngine;

public class ExplodeArea : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SphereCollider explodeArea;
    private PlayerMove player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
    }
}
