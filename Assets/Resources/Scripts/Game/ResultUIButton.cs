using UnityEngine;
using System.Collections;

public class ResultUIButton : MonoBehaviour {

    public void RetryButtonClick()
    {
        SceneChange();

        Application.LoadLevel("sceneGame");
    }

    public void NextButtonClick()
    {
        SceneChange();

        Application.LoadLevel("sceneList");
    }


    private void SceneChange()
    {
        NoteJudgeReceiver.Instance.ClearJudgeReceiver();
        AudioManager.Instance.Stop();
    }
}
