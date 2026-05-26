using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BossStageScene()
    {
        GameOverAudioManager.instance.PlayButtonClick();
        SceneManager.LoadScene("BossScene");
    }
    public void QuitGame()
    {
        GameOverAudioManager.instance.PlayButtonClick();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
