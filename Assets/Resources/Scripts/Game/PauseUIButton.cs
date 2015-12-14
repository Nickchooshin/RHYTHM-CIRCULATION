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
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }

    public void RetryButtonClick()
    {
        Application.LoadLevel("sceneGame");
    }

    public void ListButtonClick()
    {
        Application.LoadLevel("sceneSelect");
    }

    public void MainButtonClick()
    {
        Application.LoadLevel("sceneTitle");
    }
}
