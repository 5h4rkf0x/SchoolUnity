using UnityEngine;

public class TitleSceneAudioManager: MonoBehaviour
{
    public static TitleSceneAudioManager instance;

    [Header("Sources")]
    [SerializeField] private AudioSource titleBGMSource;
    [SerializeField] private AudioSource buttonClickSource;
    [SerializeField] private AudioClip buttonClickClip;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        titleBGMSource.clip = clip;
        titleBGMSource.Play();
    }
    public void StopBGM()
    {
        titleBGMSource.Stop();
    }

    public void PlayButtonClick()
    {
        titleBGMSource.Stop();
        buttonClickSource.PlayOneShot(buttonClickClip);
    }
}
