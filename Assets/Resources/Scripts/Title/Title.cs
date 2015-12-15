using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    
    void Start()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(720, 1280, false);
        //Screen.SetResolution(359, 639, false);
    }
}
