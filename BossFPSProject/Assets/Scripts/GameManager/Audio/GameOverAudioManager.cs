using UnityEngine;

public class GameOverAudioManager : MonoBehaviour
{
    public static GameOverAudioManager instance;

    [Header("Sources")]
    [SerializeField] private AudioSource gameOverSource;
    [SerializeField] private AudioSource buttonClickSource;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip buttonClickClip;

    private void Awake()
    {
        instance = this;
        PlayGameOver();
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void PlayGameOver()
    {
        gameOverSource.PlayOneShot(gameOverClip);
    }
    public void PlayButtonClick()
    {
        buttonClickSource.PlayOneShot(buttonClickClip);
    }
}
