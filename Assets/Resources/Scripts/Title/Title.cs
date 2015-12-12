using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    
    void Start()
    {
        Screen.fullScreen = false;
        //Screen.SetResolution(720, 1280, false);
        //Screen.SetResolution(359, 639, false);
    }
	
    /*
    void Update()
    {
        // Mobile
        if (Input.touchCount > 0)
            Application.LoadLevel("sceneGame");

        // PC
        if (Input.GetMouseButtonDown(0))
            Application.LoadLevel("sceneGame");
	}
    */
}
