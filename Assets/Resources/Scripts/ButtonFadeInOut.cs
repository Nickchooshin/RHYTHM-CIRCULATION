using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonFadeInOut : MonoBehaviour
{
    public Image buttonImage;
    public float fadeInOutTime = 1.0f;

    void OnEnable()
    {
        StartCoroutine("FadeInOut");
    }

    private IEnumerator FadeInOut()
    {
        float endTime = Time.time + fadeInOutTime;
        bool isFadeIn = true;

        while (true)
        {
            float alpha = ((endTime - Time.time) / fadeInOutTime);

            if (alpha < 0.0f)
            {
                endTime += fadeInOutTime;
                isFadeIn = !isFadeIn;
                alpha = 1.0f - alpha;
            }

            if (isFadeIn)
                buttonImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - alpha);
            else
                buttonImage.color = new Color(1.0f, 1.0f, 1.0f, alpha);

            yield return null;
        }
    }
}
