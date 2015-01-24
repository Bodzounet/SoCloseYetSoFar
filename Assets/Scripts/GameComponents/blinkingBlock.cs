using UnityEngine;
using System.Collections;

public class                blinkingBlock : MonoBehaviour 
{   
    BoxCollider2D           _bc2D;
    SpriteRenderer          _sr;
    Color                   _color;
    Color                   _visible;
    Color                   _invisible;

    float                   _lerpingVal = 0;

    bool                    _isPopping = false;

	void Start () 
    {
        _bc2D = GetComponent<BoxCollider2D>();
        _color = GetComponent<SpriteRenderer>().color;
        _sr = GetComponent<SpriteRenderer>();
        _visible = new Color(_color.r, _color.g, _color.b, 1);
        _invisible = new Color(_color.r, _color.g, _color.b, 0);
        StartCoroutine("changeState");
	}

    void Update()
    {
        if (_isPopping)
            pop();
        else
            depop();
        _lerpingVal += Time.deltaTime * 1.5f;
        Debug.Log(_sr.color);
    }

    void pop()
    {
        Debug.Log("coucou");
        _bc2D.enabled = true;
        _sr.color = Color.Lerp(_invisible, _visible, _lerpingVal);
    }

    void depop()
    {
        Debug.Log("caca");
        if (_sr.color.a == 0)
            _bc2D.enabled = false;
        _sr.color = Color.Lerp(_visible, _invisible, _lerpingVal);
    }

    IEnumerator changeState()
    {
        _lerpingVal = 0;
        yield return new WaitForSeconds(2);
        _isPopping = !_isPopping;
        StartCoroutine("changeState");
    }
}
