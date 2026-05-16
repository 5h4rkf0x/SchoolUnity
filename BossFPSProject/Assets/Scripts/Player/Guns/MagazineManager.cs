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
    private int index = 0;

    private int currentAmmo;
    [SerializeField] private bool isReloading;

    private List<BulletController> bulletPool = new();

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => maxAmmo;
    public bool IsReloading => isReloading;

    private void Awake()
    {
        currentAmmo = maxAmmo;

        for (int i = 0; i < poolSize; i++)
        {
            BulletController bullet = Instantiate(bulletPrefab, transform);
            bulletPool.Add(bullet);
            bullet.Init(this);
            bullet.gameObject.SetActive(false);
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
            while (bulletPool[index].gameObject.activeSelf)
            {
                index = (index + 1) % bulletPool.Count;
            }
            BulletController bullet = bulletPool[index];
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        // 풀 부족 시 추가 생성
        BulletController newBullet = Instantiate(bulletPrefab, transform);
        newBullet.Init(this);
        newBullet.gameObject.SetActive(true);

        return newBullet;
    }

    public void Reload()
    {
        if (isReloading) return;
        if (currentAmmo == maxAmmo) return;
        isReloading = true;
    }

    private void RefreshAmmoInfo()
    {
        ammoInfo.text = currentAmmo + " / 30";
    }

    public void ReloadEndSet()
    {
        currentAmmo = maxAmmo;
        RefreshAmmoInfo();
        isReloading = false;

        Debug.Log("재장전 완료");
    }
}