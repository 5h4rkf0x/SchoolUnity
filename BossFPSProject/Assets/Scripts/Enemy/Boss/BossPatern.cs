using UnityEngine;

public class BossPatern : MonoBehaviour
{
    [SerializeField] public PlayerMove player;

    void Awake()
    {
        player = FindFirstObjectByType<PlayerMove>();
    }
    
    private void FloorBombAttack()   // 지형 원형으로 랜덤하게 파괴 생성
    {

    }
}
