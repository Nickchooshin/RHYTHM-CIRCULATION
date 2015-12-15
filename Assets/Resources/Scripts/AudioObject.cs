using UnityEngine;
using System.Collections;

public class AudioObject : MonoBehaviour {

    //void Start()
    void Awake()
    {
        AudioManager.Instance.SetAudioSource(this.GetComponent<AudioSource>());
    }
}
