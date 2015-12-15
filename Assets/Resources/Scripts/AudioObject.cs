using UnityEngine;
using System.Collections;

public class AudioObject : MonoBehaviour {

    void Start()
    {
        AudioManager.Instance.SetAudioSource(this.GetComponent<AudioSource>());
    }
}
