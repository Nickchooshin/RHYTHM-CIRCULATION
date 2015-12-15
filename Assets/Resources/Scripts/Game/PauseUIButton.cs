using UnityEngine;
using System.Collections;

public class PauseUIButton : MonoBehaviour {

    private bool m_isPause = false;

    public GameObject PauseUIPanel;

    public void GamePause()
    {
        m_isPause = !m_isPause;
        PauseUIPanel.SetActive(m_isPause);

        if (m_isPause)
        {
            AudioManager.Instance.Pause();
            Time.timeScale = 0.0f;
        }
        else
        {
            AudioManager.Instance.UnPause();
            Time.timeScale = 1.0f;
        }
    }

    public void RetryButtonClick()
    {
        SceneChange();

        Application.LoadLevel("sceneGame");
    }

    public void ListButtonClick()
    {
        SceneChange();

        Application.LoadLevel("sceneList");
    }

    public void MainButtonClick()
    {
        SceneChange();

        Application.LoadLevel("sceneTitle");
    }

    private void SceneChange()
    {
        Time.timeScale = 1.0f;
        NoteJudgeReceiver.Instance.ClearJudgeReceiver();
        AudioManager.Instance.Stop();
    }
}
