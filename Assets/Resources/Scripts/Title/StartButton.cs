using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour {

    public Text buttonText;

    void Start()
    {
        StartCoroutine("FadeInOut");
    }

    private IEnumerator FadeInOut()
    {
        const float fadeTime = 1.0f;
        float endTime = Time.time + fadeTime;
        bool isFadeIn = true;

        Debug.Log("FadeInOut");
        Debug.Log("endTime = " + endTime);
        Debug.Log("time = " + Time.time);

        while (true)
        {
            float alpha = ((endTime - Time.time) / fadeTime);

            if (alpha < 0.0f)
            {
                endTime += fadeTime;
                isFadeIn = !isFadeIn;
                alpha = 1.0f - alpha;
            }

            if (isFadeIn)
                buttonText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - alpha);
            else
                buttonText.color = new Color(1.0f, 1.0f, 1.0f, alpha);

            Debug.Log(buttonText.color);

            yield return null;
        }
    }
}
