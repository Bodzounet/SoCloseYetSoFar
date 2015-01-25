using UnityEngine;
using System.Collections;

public class returnToMenu : MonoBehaviour {
	
	void Update () 
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.LoadLevel("Menu");
	}
}
