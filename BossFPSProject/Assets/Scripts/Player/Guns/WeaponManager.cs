using NUnit.Framework;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponManager: MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private Transform fireTransform;
    [SerializeField] public MagazineManager magazine;
    [SerializeField] private GameObject hitMarker;

    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float bulletSpeed = 40f;
    [SerializeField] private float bulletLifeTime = 3f;
    [SerializeField] private float maxDistance = 100f;

    private Coroutine fireCoroutine;

    private void Awake()
    {
        cam = Camera.main;
        if (cam == null) Debug.LogWarning("playerAliner.cam == null");
        hitMarker = GameObject.Find("GunUI").transform.Find("HitMarker").gameObject;
    }

    public void Reload()
    {
        magazine.Reload();
    }

    public void FireInfo()
    {
        BulletController bullet = magazine.GetBullet();

        if (bullet == null)
            return;

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

        bullet.transform.position = fireTransform.position;
        bullet.Fire(magazine.transform, shootDir, bulletSpeed, bulletLifeTime);
    }

    public void StartFire()
    {
        if (magazine.IsReloading) return;

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

    public void ShowHitUI()
    {
        StartCoroutine(OpenHitUI());
    }

    public IEnumerator AutoFire()
    {
        while (true)
        {
            FireInfo();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private IEnumerator OpenHitUI()
    {
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitMarker.SetActive(false);
    }
}