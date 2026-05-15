using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;
using System.Security.Cryptography;

public class MagazineManager : MonoBehaviour
{
    [Header("Bullet Pool")]
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private TextMeshProUGUI ammoInfo;
    [SerializeField] private int poolSize = 30;

    [Header("Magazine")]
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private float reloadTime = 1.5f;

    private int currentAmmo;
    private bool isReloading;

    private Queue<BulletController> bulletPool = new Queue<BulletController>();

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => maxAmmo;
    public bool IsReloading => isReloading;

    private void Awake()
    {
        currentAmmo = maxAmmo;

        for (int i = 0; i < poolSize; i++)
        {
            BulletController bullet = Instantiate(bulletPrefab, transform);
            bullet.Init(this);
            bullet.gameObject.SetActive(false);

            bulletPool.Enqueue(bullet);
        }
    }

    private void Start()
    {
        RefreshAmmoInfo();
    }

    public BulletController GetBullet()
    {
        if (isReloading)
            return null;

        if (currentAmmo <= 0)
        {
            Debug.Log("탄약 없음");
            return null;
        }

        currentAmmo--;
        RefreshAmmoInfo();
        if (bulletPool.Count > 0)
        {
            BulletController bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        // 풀 부족 시 추가 생성
        BulletController newBullet = Instantiate(bulletPrefab, transform);
        newBullet.Init(this);
        newBullet.gameObject.SetActive(true);

        return newBullet;
    }

    public void ReturnBullet(BulletController bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    public void Reload()
    {
        if (isReloading) return;
        if (currentAmmo == maxAmmo) return;

        StartCoroutine(ReloadRoutine());
    }

    private void RefreshAmmoInfo()
    {
        ammoInfo.text = currentAmmo + " / 30";
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        Debug.Log("재장전 시작");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        RefreshAmmoInfo();
        isReloading = false;

        Debug.Log("재장전 완료");
    }
}