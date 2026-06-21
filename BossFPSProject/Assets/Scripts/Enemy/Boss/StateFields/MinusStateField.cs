using UnityEngine;

public class MinusStateField : MonoBehaviour
{
    [SerializeField] private Player player;
    private bool isCollided = false;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckPlayer(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckPlayer(other);
    }

    private void CheckPlayer(Collider other)
    {
        if (other.CompareTag("Player") && !isCollided)
        {
            isCollided = true;
            if (player.CurrentPlayerState == PlayerStates.Minus)
            {
                player.Heal(30);
            }
            else
            {
                player.TakeDamage(60);
            }
        }
    }

    private void OnDisable()
    {
        isCollided = false;
    }
}