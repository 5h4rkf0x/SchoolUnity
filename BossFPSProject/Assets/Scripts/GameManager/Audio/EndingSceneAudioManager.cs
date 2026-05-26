using UnityEngine;

public class EndingSceneAudioManager: MonoBehaviour
{
    public static EndingSceneAudioManager instance;

    [Header("Sources")]
    [SerializeField] private AudioSource endingBGMSource;
    [SerializeField] private AudioSource buttonClickSource;
    [SerializeField] private AudioClip buttonClickClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayBGM(AudioClip clip)
    {
        endingBGMSource.clip = clip;
        endingBGMSource.Play();
    }
    public void StopBGM()
    {
        endingBGMSource.Stop();
    }

    public void PlayButtonClick()
    {
        buttonClickSource.PlayOneShot(buttonClickClip);
    }
}
