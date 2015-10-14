using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    
    void Start()
    {
    }
	
    void Update()
    {
        // Mobile
        if (Input.touchCount > 0)
            Application.LoadLevel("sceneGame");

        // PC
        /*
        if (Input.GetMouseButtonDown(0))
            Application.LoadLevel("sceneGame");
        */
	}
}
