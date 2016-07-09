using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    private static AudioManager m_instance = null;

    public AudioSource audioSource_BGM;
    public AudioSource audioSource_SE;
    
    private AudioClip m_audioClip = null;
    private AudioClip m_seClip = null;

    private float m_startTime = 0.0f;
    private float m_endTime = 0.0f;
    private bool m_loop = false;

    private Dictionary<string, AudioClip> m_seList = new Dictionary<string, AudioClip>();

    public static AudioManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType(typeof(AudioManager)) as AudioManager;
            }

            return m_instance;
        }
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

        if (audioSource_BGM != null)
        {
            if (audioSource_BGM.clip != m_audioClip)
                audioSource_BGM.clip = m_audioClip;

            audioSource_BGM.Play();
        }
    }

    public void Stop()
    {
        if (audioSource_BGM != null)
        {
            audioSource_BGM.time = 0.0f;
            audioSource_BGM.Stop();
        }
    }

    public void Pause()
    {
        if (audioSource_BGM != null)
            audioSource_BGM.Pause();
    }

    public void UnPause()
    {
        if (audioSource_BGM != null)
            audioSource_BGM.UnPause();
    }

    //public void PlaySection(float startTime, float endTime, bool loop, float fadeInOutTime)
    public void PlaySection(float startTime, float endTime, bool loop)
    {
        if (m_audioClip != null)
        {
            while (m_audioClip.loadState != AudioDataLoadState.Loaded)
            {
            }
        }

        if (audioSource_BGM != null)
        {
            if (audioSource_BGM.clip != m_audioClip)
                audioSource_BGM.clip = m_audioClip;

            m_startTime = startTime;
            m_endTime = endTime;
            m_loop = loop;
            
            StartCoroutine("Play_Section");

            audioSource_BGM.time = m_startTime;
            audioSource_BGM.Play();
        }
    }

    public float GetTime()
    {
        if (audioSource_BGM != null)
            return audioSource_BGM.time;

        return 0.0f;
    }

    public float GetLength()
    {
        if (m_audioClip != null)
            return m_audioClip.length;

        return 0.0f;
    }

    private IEnumerator Play_Section()
    {
        AudioClip audioClip = audioSource_BGM.clip;
        float volume = audioSource_BGM.volume;

        while (audioSource_BGM.clip == audioClip)
        {
            /*
            if (m_audioSource.time < (m_startTime + m_fadeInOutTime))
                m_audioSource.volume = (((m_startTime + m_fadeInOutTime) - m_audioSource.time) / m_fadeInOutTime) * volume;
            else if (m_audioSource.time > (m_endTime - m_fadeInOutTime))
                m_audioSource.volume = ((m_audioSource.time - (m_endTime - m_fadeInOutTime)) / m_fadeInOutTime) * volume;
            else
                m_audioSource.volume = volume;
            */

            if (audioSource_BGM.time >= m_endTime)
            {
                if (m_loop)
                    audioSource_BGM.time = m_startTime;
                else
                    audioSource_BGM.Stop();
            }

            yield return null;
        }
    }

    // SE
    public void SetSEClip(AudioClip seClip)
    {
        m_seClip = seClip;
    }

    public void SetSEClip(string seClip)
    {
        m_seClip = Resources.Load<AudioClip>("Sounds/" + seClip);
    }

    public void PlaySE()
    {
        if (m_seClip != null)
        {
            while (m_seClip.loadState != AudioDataLoadState.Loaded)
            {
            }
        }

        if (audioSource_SE != null)
        {
            if (audioSource_SE.clip != m_seClip)
                audioSource_SE.clip = m_seClip;

            audioSource_SE.Play();
        }
    }

    public void PlaySE(string key)
    {
        if (audioSource_SE != null && m_seList.ContainsKey(key))
        {
            audioSource_SE.clip = m_seList[key];
            audioSource_SE.Play();
        }
    }

    public void StopSE()
    {
        if (audioSource_SE != null)
        {
            audioSource_SE.time = 0.0f;
            audioSource_SE.Stop();
        }
    }

    public void PauseSE()
    {
        if (audioSource_SE != null)
            audioSource_SE.Pause();
    }

    public void UnPauseSE()
    {
        if (audioSource_SE != null)
            audioSource_SE.UnPause();
    }

    public void LoadSEClip(AudioClip seClip, string key)
    {
        m_seList[key] = seClip;
    }

    public void LoadSEClip(string seClip, string key)
    {
        m_seList[key] = Resources.Load<AudioClip>("Sounds/" + seClip);
    }

    public void ClearLoadSEClips()
    {
        m_seList.Clear();
    }
}
