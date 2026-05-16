using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        health = maxHealth;
    }

    public void TakeDamage(float Damage)
    {
        health -= Damage;
        // hpUI.SetHP(health, maxHealth);

        Debug.Log(health);

        if (health <= 0)
        {
            Die();
            // hpUI.CloseBossBar();
        }
    }

    public void ExplodeDamage(float Damage)
    {
        health -= Damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() // 샌즈처럼 검은화면에서 player가 분해되면 재밌긴 할듯 ㅋㅋ -------------- 씬전환기 넣어야될 위치
    {
        gameObject.SetActive(false);
    }
}
