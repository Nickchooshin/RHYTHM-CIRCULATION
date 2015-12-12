using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler {

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

            yield return null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Application.LoadLevel("sceneGame");
    }
}
