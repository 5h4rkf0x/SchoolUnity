using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Assertions.Must;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider coll;
    [SerializeField] private Transform parent;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private AudioClip hitClip;

    private WeaponManager weapon;
    private Coroutine lifeCoroutine;

    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;

    private void Awake()
    {
        weapon = FindFirstObjectByType<WeaponManager>();
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        coll = GetComponent<CapsuleCollider>();
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
            AudioManager.instance.PlaySFX(hitClip);
            Boss Boss = collision.transform.root.GetComponent<Boss>();
    
            Boss.TakeDamage(bulletDamage);
            weapon.ShowHitUI();
        }
        transform.parent = parent;
        trail.emitting = false;
        trail.Clear();
        gameObject.SetActive(false);
    }

    public void Fire(Transform parent, Vector3 dir, float speed, float lifeTime)
    {
        trail.emitting = true;
        this.parent = parent;
        transform.SetParent(null);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.LookRotation(dir);
        rb.linearVelocity = dir.normalized * speed;

        if (lifeCoroutine != null)
            StopCoroutine(lifeCoroutine);

        lifeCoroutine = StartCoroutine(ReturnAfterTime(lifeTime));
    }

    // private void IfBossHit()
    // {
    //     Vector3 currentDir = rb.linearVelocity.normalized;
    //     Vector3 point1 = transform.TransformPoint(coll.center) + transform.up * coll.height / 2 - coll.radius);
    //     Vector3 point2 = transform.TransformPoint(coll.center) - transform.up * coll.height / 2 - coll.radius);
    //     float raycastDistance = (currentDir * rb.linearVelocity.magnitude * Time.fixedDeltaTime).magnitude;
    // 
    //     RaycastHit hit = Physics.CapsuleCast(point1, point2, coll.radius, currentDir, raycastDistance);
    // 
    // }

    private IEnumerator ReturnAfterTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        transform.SetParent(parent);
        trail.emitting = false;
        trail.Clear();
        gameObject.SetActive(false);
    }
}