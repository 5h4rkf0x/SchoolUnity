using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider coll;
    [SerializeField] private Transform parent;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private AudioClip hitClip;

    private WeaponManager weapon;
    private Coroutine lifeCoroutine;

    [SerializeField] private LayerMask bossLayer;

    [SerializeField] private bool usePredictCast = true;

    [SerializeField] private float bulletDamage;

    private bool returned;

    private void Awake()
    {
        weapon = FindFirstObjectByType<WeaponManager>();
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        coll = GetComponent<CapsuleCollider>();
        bossLayer = LayerMask.GetMask("Boss");
    }

    private void FixedUpdate()
    {
        if (!gameObject.activeInHierarchy) return;
        if (!usePredictCast) return;

        IfBossHit();
    }

    public void Init(MagazineManager magazineManager)
    {
        weapon.magazine = magazineManager;
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy == null) return;
            enemy.TakeDamage(bulletDamage);
            weapon.ShowHitUI();
            ReturnBullet();
        }
    }

    public void Fire(Transform parent, Vector3 dir, float speed, float lifeTime)
    {
        returned = false;

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

    private void IfBossHit()
    {
        Vector3 velocity = rb.linearVelocity;

        if (velocity.sqrMagnitude <= 0.0001f)
            return;

        float dt = Time.fixedDeltaTime;

        Vector3 acceleration = rb.useGravity ? Physics.gravity : Vector3.zero;

        // 포물선 이동 예측: 현재 속도 + 중력 가속도
        Vector3 predictedMove = velocity * dt + 0.5f * acceleration * dt * dt;

        float castDistance = predictedMove.magnitude;

        if (castDistance <= 0.0001f)
            return;

        Vector3 castDir = predictedMove.normalized;

        GetCapsuleWorldPoints(out Vector3 point1, out Vector3 point2, out float radius);

        if (Physics.CapsuleCast(point1, point2, radius, castDir, out RaycastHit hit, castDistance, bossLayer, QueryTriggerInteraction.Collide))
        {
            if (!hit.transform.root.CompareTag("Boss"))
                return;

            Debug.Log("보스 예측 타격!");

            AudioManager.instance.PlaySFX(hitClip);

            Boss boss = hit.transform.root.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(bulletDamage);
                weapon.ShowHitUI();
            }

            ReturnBullet();
        }
    }

    private void GetCapsuleWorldPoints(out Vector3 point1, out Vector3 point2, out float radius)
    {
        Vector3 center = transform.TransformPoint(coll.center);

        Vector3 direction;
        float heightScale;
        float radiusScale;

        switch (coll.direction)
        {
            case 0: // X axis
                direction = transform.right;
                heightScale = Mathf.Abs(transform.lossyScale.x);
                radiusScale = Mathf.Max(Mathf.Abs(transform.lossyScale.y), Mathf.Abs(transform.lossyScale.z));
                break;

            case 1: // Y axis
                direction = transform.up;
                heightScale = Mathf.Abs(transform.lossyScale.y);
                radiusScale = Mathf.Max(Mathf.Abs(transform.lossyScale.x), Mathf.Abs(transform.lossyScale.z));
                break;

            default: // Z axis
                direction = transform.forward;
                heightScale = Mathf.Abs(transform.lossyScale.z);
                radiusScale = Mathf.Max(Mathf.Abs(transform.lossyScale.x), Mathf.Abs(transform.lossyScale.y));
                break;
        }

        radius = coll.radius * radiusScale;

        float height = Mathf.Max(coll.height * heightScale, radius * 2f);
        float halfSegment = height * 0.5f - radius;

        point1 = center + direction * halfSegment;
        point2 = center - direction * halfSegment;
    }

    private void ReturnBullet()
    {
        if (returned) return;
        returned = true;

        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }

        transform.parent = parent;
        trail.emitting = false;
        trail.Clear();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        gameObject.SetActive(false);
    }

    private IEnumerator ReturnAfterTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnBullet();
    }
}