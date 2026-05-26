using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScene : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private Player player;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
        boss = FindFirstObjectByType<Boss>();
    }

    private void Update()
    {
        if (boss.Health <= 0)
        {
            GoToEnding();
        }
        else if (player.Health <= 0)
        {
            GoToGameOver();
        }
    }

    public void GoToEnding()
    {
        AudioManager.instance.StopBGM();
        AudioManager.instance.StopSFX();
        SceneManager.LoadScene("EndingScene");
    }
    public void GoToGameOver()
    {
        AudioManager.instance.StopBGM();
        AudioManager.instance.StopSFX();
        SceneManager.LoadScene("GameOverScene");
    }
}
