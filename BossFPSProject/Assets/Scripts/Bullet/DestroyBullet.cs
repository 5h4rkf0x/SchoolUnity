using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
    }
}
