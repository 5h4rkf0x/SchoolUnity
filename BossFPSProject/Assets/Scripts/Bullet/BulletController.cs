using UnityEngine;

public class BulletController : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage();
        }
    }
}
