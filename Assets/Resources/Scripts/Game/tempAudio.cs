using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tempAudio : MonoBehaviour {

    private AudioClip audioClip;

    public AudioSource audioSource;

    void Start()
    {
        /*
        WWW www = new WWW("file://" + Application.dataPath + "/music.ogg");
        audioClip = www.audioClip;

        audioSource.clip = audioClip;
        while (!audioClip.isReadyToPlay)
        {
        }
        audioSource.Play();
        */
    }
}
