using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class Player : MonoBehaviour
{
    public static Player instance;
    public enum PlayerStates
    {
        Plus,
        Minus
    }

    [SerializeField] private PlayerHpBar hpUI;
    [SerializeField] private GameObject plusStateUI;
    [SerializeField] private GameObject minusStateUI;

    private PlayerStates currentPlayerState = PlayerStates.Plus;

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

        plusStateUI = GameObject.Find("+");
        minusStateUI = GameObject.Find("-");
    }

    private void Start()
    {
        hpUI.SetHP(health, maxHealth);
        minusStateUI.gameObject.SetActive(false);
    }

    public void ChangeState()
    {
        if (currentPlayerState == PlayerStates.Plus)
        {
            currentPlayerState = PlayerStates.Minus;
            plusStateUI.gameObject.SetActive(false);
            minusStateUI.gameObject.SetActive(true);
        }
        else
        {
            currentPlayerState = PlayerStates.Plus;
            plusStateUI.gameObject.SetActive(true);
            minusStateUI.gameObject.SetActive(false);
        }
    }

    public void Heal(float HealAmount)
    {
        health += HealAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
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
