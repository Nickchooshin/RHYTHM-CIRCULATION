using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    private static readonly AudioManager m_instance = new AudioManager();

    private AudioSource m_audioSource = null;
    private AudioClip m_audioClip = null;

    public static AudioManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    public void SetAudioSource(AudioSource audioSource)
    {
        m_audioSource = audioSource;
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        m_audioClip = audioClip;
    }

    public void SetAudioClip(string audioClip)
    {
        m_audioClip = Resources.Load<AudioClip>("Sounds/" + audioClip);
    }

    public void Play()
    {
        if (m_audioClip != null)
        {
            while (m_audioClip.loadState != AudioDataLoadState.Loaded)
            {
            }
        }

        if (m_audioSource != null)
        {
            if (m_audioSource.clip != m_audioClip)
                m_audioSource.clip = m_audioClip;
            m_audioSource.Play();
        }
    }

    public void Stop()
    {
        if (m_audioSource != null)
            m_audioSource.Stop();
    }

    public void Pause()
    {
        if (m_audioSource != null)
        {
            m_audioSource.Pause();
        }
    }

    public void UnPause()
    {
        if (m_audioSource != null)
        {
            m_audioSource.UnPause();
        }
    }

    public float GetTime()
    {
        if (m_audioSource != null)
            return m_audioSource.time;

        return 0.0f;
    }
}
