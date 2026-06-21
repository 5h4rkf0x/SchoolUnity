using UnityEngine;

public class PlusStateCube : MonoBehaviour
{
    [SerializeField] private CubeManager cubeManager;
    private Rigidbody rb;
    
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask bossLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cubeManager = FindFirstObjectByType<CubeManager>();
        playerLayer = LayerMask.GetMask("Player");
        bossLayer = LayerMask.GetMask("Boss");
    }

    private void FixedUpdate()
    {
        Move(cubeManager.cubeSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsInLayerMask(other.gameObject, playerLayer) && Player.instance.CurrentPlayerState == PlayerStates.Plus)
        {
            cubeManager.NotifyCubeRemoved(gameObject);
            gameObject.SetActive(false);
        }
        else if (IsInLayerMask(other.gameObject, bossLayer))
        {
            Player.instance.TakeDamage(100);
            cubeManager.NotifyCubeRemoved(gameObject);
            gameObject.SetActive(false);
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) != 0;
    }

    private void Move(float speed)
    {
        Vector3 dir = cubeManager.transform.position - transform.position;
        dir.Normalize();
        rb.linearVelocity = dir * speed;
    }
}
