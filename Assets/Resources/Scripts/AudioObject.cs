using UnityEngine;
using System.Collections;

public class AudioObject : MonoBehaviour {

    private AudioSource m_audioSource = null;

    private float m_startTime = 0.0f;
    private float m_endTime = 0.0f;
    private bool m_loop = false;
    //private float m_fadeInOutTime = 0.0f;

    //void Start()
    void Awake()
    {
        m_audioSource = this.GetComponent<AudioSource>();

        AudioManager.Instance.SetAudioObject(this);
    }

    //public void PlaySection(float startTime, float endTime, bool loop, float fadeInOutTime
    public void PlaySection(float startTime, float endTime, bool loop)
    {
        m_startTime = startTime;
        m_endTime = endTime;
        m_loop = loop;
        //m_fadeInOutTime = fadeInOutTime;

        StartCoroutine("Play");

        m_audioSource.time = m_startTime;
        m_audioSource.Play();
    }

    private IEnumerator Play()
    {
        AudioClip audioClip = m_audioSource.clip;
        float volume = m_audioSource.volume;

        while (m_audioSource.clip == audioClip)
        {
            /*
            if (m_audioSource.time < (m_startTime + m_fadeInOutTime))
                m_audioSource.volume = (((m_startTime + m_fadeInOutTime) - m_audioSource.time) / m_fadeInOutTime) * volume;
            else if (m_audioSource.time > (m_endTime - m_fadeInOutTime))
                m_audioSource.volume = ((m_audioSource.time - (m_endTime - m_fadeInOutTime)) / m_fadeInOutTime) * volume;
            else
                m_audioSource.volume = volume;
            */

            if (m_audioSource.time >= m_endTime)
            {
                if (m_loop)
                    m_audioSource.time = m_startTime;
                else
                    m_audioSource.Stop();
            }

            yield return null;
        }
    }
}
