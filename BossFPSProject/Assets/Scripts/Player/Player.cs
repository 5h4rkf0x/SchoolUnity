using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public enum PlayerStates
    {
        Plus,
        Minus
    }

    [SerializeField] private PlayerHpBar hpUI;

    private PlayerStates currentPlayerState;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;

    public float Health => health;
    public PlayerStates CurrentPlayerState => currentPlayerState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Cursor.lockState = CursorLockMode.Locked;
        hpUI = FindFirstObjectByType<PlayerHpBar>();
        health = maxHealth;
    }

    public void ChangeState()
    {
        if (currentPlayerState == PlayerStates.Plus)
        {
            currentPlayerState = PlayerStates.Minus;
        }
        else
        {
            currentPlayerState = PlayerStates.Plus;
        }
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
