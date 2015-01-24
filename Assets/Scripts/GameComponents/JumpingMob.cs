using UnityEngine;
using System.Collections;

public class            JumpingMob : MonoBehaviour
{
    public float        _jumpingSpeed;
    private bool        _isJumping = false;
    private bool        _unique = true;    

	void Update () 
    {
        if (!_isJumping)
        {
            _isJumping = true;
            rigidbody2D.velocity = new Vector2(0, _jumpingSpeed);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" && _unique)
            StartCoroutine("rejump");
    }

    IEnumerator rejump()
    {
        _unique = false;
        yield return new WaitForSeconds(3);
        _isJumping = false;
        _unique = true;

    }
}
