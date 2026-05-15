using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float bulletDamage;

    private WeaponManager weapon;
    private Coroutine lifeCoroutine;

    private void Awake()
    {
        weapon = FindFirstObjectByType<WeaponManager>();
        rb = GetComponent<Rigidbody>();
    }
    public void Init(MagazineManager magazineManager)
    {
        weapon.magazine = magazineManager;
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
    
            enemy.TakeDamage(bulletDamage);
            weapon.ShowHitUI();
        }
        else if (collision.transform.root.CompareTag("Boss"))
        {
            Debug.Log("보스 타격!");
            Boss Boss = collision.transform.root.GetComponent<Boss>();
    
            Boss.TakeDamage(bulletDamage);
            weapon.ShowHitUI();
        }
        ReturnToMagazine();
    }

    public void Fire(Vector3 dir, float speed, float lifeTime)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.LookRotation(dir);
        rb.linearVelocity = dir.normalized * speed;

        if (lifeCoroutine != null)
            StopCoroutine(lifeCoroutine);

        lifeCoroutine = StartCoroutine(ReturnAfterTime(lifeTime));
    }

    private IEnumerator ReturnAfterTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToMagazine();
    }

    private void ReturnToMagazine()
    {
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        weapon.magazine.ReturnBullet(this);
    }
}