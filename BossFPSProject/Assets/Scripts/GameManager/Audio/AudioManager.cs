using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource explodeSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // 효과음 재생
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    // 배경음 재생
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    // 배경음 정지
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlayExplode(AudioClip clip)
    {
        explodeSource.PlayOneShot(clip);
    }
}