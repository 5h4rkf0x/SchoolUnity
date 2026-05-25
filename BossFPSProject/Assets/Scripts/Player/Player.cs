using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerHpBar hpUI;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;

    public float Health => health;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        hpUI = FindFirstObjectByType<PlayerHpBar>();
        health = maxHealth;
    }

    private void Start()
    {
        hpUI.SetHP(health, maxHealth);
    }

    public void TakeDamage(float Damage)
    {
        health -= Damage;
        hpUI.SetHP(health, maxHealth);

        Debug.Log(health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void ExplodeDamage(float Damage)
    {
        health -= Damage;
        hpUI.SetHP(health, maxHealth);
        if (health <= 0)
        {
            Die();
            hpUI.CloseBossBar();
        }
    }

    private void Die() // 샌즈처럼 검은화면에서 player가 분해되면 재밌긴 할듯 ㅋㅋ
    {
        gameObject.SetActive(false);
    }
}
