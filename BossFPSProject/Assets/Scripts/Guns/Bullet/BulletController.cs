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
        Debug.Log("무언가와 충돌함!");
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(bulletDamage);
        }
        else if (collision.transform.root.CompareTag("Boss"))
        {
            Debug.Log("보스 타격!");
            Boss Boss = collision.transform.root.GetComponent<Boss>();

            Boss.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
