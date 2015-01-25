using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {

    Color from = Color.white;
    Color to = Color.red;

    TextMesh me;
    private float timer;

    bool kikoo = false;

	// Use this for initialization
	void Start () 
    {
        me = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (kikoo)
            me.color = Color.Lerp(from, to, timer);
        timer += Time.deltaTime;
        if (timer > 1)
            kikoo = false;
	}

    void OnMouseEnter()
    {
        kikoo = true;
        from = me.color;
        to = Color.red;
        timer = 0;
    }

    void OnMouseExit()
    {
        kikoo = true;
        from = me.color;
        to = Color.white;
        timer = 0;
    }
}
