using UnityEngine;

public class PlusStateField : MonoBehaviour
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
            // 플레이어가 +필드에 들어왔을 때의 효과를 여기에 구현
            if (player.CurrentPlayerState == Player.PlayerStates.Plus)
            {
                player.Heal(20);
            }
            else
            {
                player.TakeDamage(70);
            }
            // 예: 플레이어의 체력을 회복하거나, 공격력을 증가시키는 등의 효과
        }
    }
}
