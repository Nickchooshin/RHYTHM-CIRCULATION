using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    
    void Start()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(720, 1280, false);
#if UNITY_EDITOR_WIN
        Screen.SetResolution(359, 639, false);
#endif
    }
}
