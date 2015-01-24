using UnityEngine;
using System.Collections;

public class                        CharacterController : MonoBehaviour 
{
    public float                    _speed;                                 // speed of the character
    public float                    _jumpSpeed;                             // jumping intensity
    
    private bool                    _grounded = false;                      // jump
    private bool                    _climbing = false;                      // climb
    
    private float                   _gravityScale;                          // since it is set to 0 when we are on a ladder, we must remember it to reset it correctly when we leave the ladder

	void Start () 
    {
	
	}
	
	void Update () 
    {
        float actualSpeed = (_climbing && !_grounded) ? _speed / 5 : _speed;
        float HAxis = Input.GetAxis("Horizontal");
        float VAxis = Input.GetAxis("Vertical");

        Vector2 newVelocity = rigidbody2D.velocity;

        if (HAxis > 0)
            newVelocity.x = actualSpeed;
        else if (HAxis < 0)
            newVelocity.x = -actualSpeed;
        else if (_grounded || _climbing)
            newVelocity = Vector2.zero;

        if (_climbing)
            if (VAxis > 0)
                newVelocity.y = _jumpSpeed;
            else if (VAxis < 0)
                newVelocity.y = -_jumpSpeed;
            else
                newVelocity.y = 0;

        if ((_grounded || _climbing) && Input.GetAxis("Jump") > 0)
        {
            _grounded = false;
            newVelocity.y = _jumpSpeed;
        }

        rigidbody2D.velocity = newVelocity;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            _grounded = true;
        else if (col.gameObject.tag == "Moveable")
            col.gameObject.rigidbody2D.velocity = new Vector2(_speed / 5, col.gameObject.rigidbody2D.velocity.y);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            _grounded = false;
        if (col.gameObject.tag == "Moveable")
            col.gameObject.rigidbody2D.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ladder")
            startClimbingMode();
    }
     

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ladder")
            endClimbingMode();
    }

    void startClimbingMode()
    {
        _climbing = true;
        _gravityScale = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = Vector2.zero;
    }

    void endClimbingMode()
    {
        _climbing = false;
        rigidbody2D.gravityScale = _gravityScale;
    }
}
