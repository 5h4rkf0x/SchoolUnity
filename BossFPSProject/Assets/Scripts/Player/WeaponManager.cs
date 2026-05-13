using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponManager: MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float bulletSpeed = 40f;
    [SerializeField] private float maxDistance = 100f;

    private Coroutine fireCoroutine;
    public bool isReloading = false;

    public void Reload()
    {
        isReloading = true;
        Debug.Log($"{gameObject.name} ¿Á¿Â¿¸");
    }

    public void Fire()
    {

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * maxDistance;
        }

        Vector3 shootDir = (targetPoint - fireTransform.position).normalized;

        Rigidbody bullet = Instantiate(bulletPrefab, fireTransform.position, Quaternion.LookRotation(shootDir));

        bullet.linearVelocity = shootDir * bulletSpeed;

        Destroy(bullet, 3f);
    }

    public void StartFire()
    {
        if (isReloading) return;

        if (fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(AutoFire());
        }
    }

    public void StopFire()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    public IEnumerator AutoFire()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
    }
}