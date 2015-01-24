﻿using UnityEngine;
using System.Collections;

public class            MovingBlock : MonoBehaviour 
{
    public enum e_dir
    {
        HORIZONTAL,
        VERTICAL
    }

    public int          _distanceInBlock;
    public e_dir        _dir = e_dir.HORIZONTAL;
    public int          _speed;

    private int         _ping = 1;

    private Vector2     _initPos;
    private Vector2     _endPos;
    private Vector2     _currentPos;

    private bool        _fromAToB = true;

    void Start()
    {
        _initPos = transform.position;
        _currentPos = _initPos;

        float incr = (_distanceInBlock) * renderer.bounds.size.x;

        if (_dir == e_dir.HORIZONTAL)
            _endPos = _initPos + new Vector2(incr, 0);
        else
            _endPos = _initPos + new Vector2(0, incr);
    }

	void Update () 
    {
        if (_dir == e_dir.HORIZONTAL)
            rigidbody2D.velocity = new Vector2(_speed * calculatePing(_currentPos.x, _initPos.x, _endPos.x), 0);
        else
            rigidbody2D.velocity = new Vector2(0, _speed * calculatePing(_currentPos.y, _initPos.y, _endPos.y));
	}

    int calculatePing(float current, float targetInf, float targetSup)
    {
        //if (current >= targetInf && _fromAToB)
        //{
        //    if ()
        //    return 1;
        //}
        //if (current <= targetSup && !)
        //    _ping = -1;
        return _ping;
    }
}