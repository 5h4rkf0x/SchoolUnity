using UnityEngine;
using UnityEngine.UI;

public class Boss: MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BossHPBar hpUI;

    [Header("Variables")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    private void Awake()
    {
        hpUI = FindFirstObjectByType<BossHPBar>();
        health = maxHealth;
    }
    private void Start()
    {
        hpUI.SetHP(health, maxHealth);
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
