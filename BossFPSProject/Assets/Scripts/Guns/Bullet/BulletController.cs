using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float BulletDamage;
    public float bulletDamage => BulletDamage;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
