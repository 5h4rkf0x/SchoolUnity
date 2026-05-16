using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss: MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BossHPBar hpUI;
    [SerializeField] KnifeManager knifeManager;
    [SerializeField] List<Transform> spawnPoints;

    [Header("Variables")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public List<Transform> SpawnPoints => spawnPoints;

    private void Awake()
    {
        hpUI = FindFirstObjectByType<BossHPBar>();
        health = maxHealth;
    }
    private void Start()
    {
        hpUI.SetHP(health, maxHealth);
        knifeManager.SpawnKnives(SpawnPoints);
    }

    public void TakeDamage(float bulletDamage)
    {
        health -= bulletDamage;
        hpUI.SetHP(health, maxHealth);

        Debug.Log(health);

        if (health <= 0)
        {
            Die();
            hpUI.CloseBossBar();
        }
    }
    
    private void Die()
    {
        Debug.Log("º¸½º »ç¸Á!@@@");
        gameObject.SetActive(false);
    }
}
