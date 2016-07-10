using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogoFadeInOut : MonoBehaviour {

    public Image teamLogo;
    public Image infoLogo;

    void Start()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(720, 1280, false);
        //Screen.SetResolution(359, 639, false);

        //HighScoreManager.Instance.LoadHighScore();

        StartCoroutine("FadeInOut");
    }

    private IEnumerator FadeInOut()
    {
        const float fadeInOutTime = 1.25f;
        float startTime = Time.time;

        // Team Logo Fade In
        while (Time.time < startTime + fadeInOutTime)
        {
            float nowTime = Time.time;
            float alpha = (nowTime - startTime) / fadeInOutTime;

            teamLogo.color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return null;
        }

        startTime = startTime + fadeInOutTime;

        // Team Logo Fade Out
        while (Time.time < startTime + fadeInOutTime)
        {
            float nowTime = Time.time;
            float alpha = 1.0f - ((nowTime - startTime) / fadeInOutTime);

            teamLogo.color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return null;
        }

        startTime = startTime + fadeInOutTime;

        teamLogo.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        // Info Logo Fade In
        while (Time.time < startTime + fadeInOutTime)
        {
            float nowTime = Time.time;
            float alpha = (nowTime - startTime) / fadeInOutTime;

            infoLogo.color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return null;
        }

        startTime = startTime + fadeInOutTime;

        // Info Logo Fade Out
        while (Time.time < startTime + fadeInOutTime)
        {
            float nowTime = Time.time;
            float alpha = 1.0f - ((nowTime - startTime) / fadeInOutTime);

            infoLogo.color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return null;
        }

        startTime = startTime + fadeInOutTime;

        infoLogo.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(0.5f);

        Application.LoadLevel("sceneTitle");
    }
}
