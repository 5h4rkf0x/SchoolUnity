using UnityEngine;

public class TitleBGMSource: MonoBehaviour
{

    [SerializeField] private AudioClip TitleBGM;

    private void Start()
    {
        TitleSceneAudioManager.instance.PlayBGM(TitleBGM);
    }
}
