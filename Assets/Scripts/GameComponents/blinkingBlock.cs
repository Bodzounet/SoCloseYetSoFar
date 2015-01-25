using UnityEngine;
using System.Collections;

public class                blinkingBlock : MonoBehaviour 
{   
    BoxCollider2D           _bc2D;
    SpriteRenderer          _sr;
    Color                   _color = Color.white;
    Color                   _visible;
    Color                   _invisible;

    float                   _lerpingVal = 0;

    bool                    _isPopping = false;

    public string msg = null;

    void Awake()
    {
        StartCoroutine("changeState");
    }

	public void Start () 
    {
        _bc2D = GetComponent<BoxCollider2D>();
        _bc2D.enabled = true;
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = new Color(1, 1, 1, 1);
        _visible = new Color(_color.r, _color.g, _color.b, 1);
        _invisible = new Color(_color.r, _color.g, _color.b, 0);
        
	}

    public void reset()
    {
        StopAllCoroutines();
        _bc2D = GetComponent<BoxCollider2D>();
        _bc2D.enabled = true;
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = new Color(1, 1, 1, 1);
        _visible = new Color(_color.r, _color.g, _color.b, 1);
        _invisible = new Color(_color.r, _color.g, _color.b, 0);
        _isPopping = false;
        _lerpingVal = 0;
        StartCoroutine("changeState");
    }

    void Update()
    {
        if (_isPopping)
            pop();
        else
            depop();
        _lerpingVal += Time.deltaTime * 1.5f;
    }

    void pop()
    {
        _bc2D.enabled = true;
        _sr.color = Color.Lerp(_invisible, _visible, _lerpingVal);
    }

    void depop()
    {
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
