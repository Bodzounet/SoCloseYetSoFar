using UnityEngine;
using System.Collections;

public class pop : MonoBehaviour {

    TextMesh tm;
    float incr = 0;

	void Start () 
    {
        tm = GetComponent<TextMesh>();
	}
	
	void Update () 
    {
        tm.color = Color.Lerp(Color.black, Color.red, incr);
        incr += Time.deltaTime / 5;
        if (incr > 1.3)
            Application.LoadLevel("menu");
	}

}
