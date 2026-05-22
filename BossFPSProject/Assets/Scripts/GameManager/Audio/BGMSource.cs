using UnityEngine;

public class BGMSource : MonoBehaviour
{

    [SerializeField] private AudioClip stageBGM;

    private void Start()
    {
        AudioManager.instance.PlayBGM(stageBGM);
    }
}
