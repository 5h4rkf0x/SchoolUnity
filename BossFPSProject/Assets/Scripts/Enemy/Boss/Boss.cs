using UnityEngine;

public class Boss: MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] private float MaxHealth;

    private void Awake()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float bulletDamage)
    {
        Health -= bulletDamage;
        Debug.Log(Health);

        if (Health <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log("Å©¾Æ¾Ç!@@@");
        gameObject.SetActive(false);
    }
}
