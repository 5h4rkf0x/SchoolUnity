using UnityEngine;

public class MinusStateField : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();    
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.CurrentPlayerState == Player.PlayerStates.Minus)
            {
                player.Heal(20);
            }
            else
            {
                player.TakeDamage(70);
            }
        }
    }
}
