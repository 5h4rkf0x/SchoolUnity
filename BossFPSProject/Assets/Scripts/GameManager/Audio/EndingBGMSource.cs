using UnityEngine;

public class EndingBGMSource: MonoBehaviour
{

    [SerializeField] private AudioClip endingBGM;

    private void Start()
    {
        EndingSceneAudioManager.instance.PlayBGM(endingBGM);
    }
}
