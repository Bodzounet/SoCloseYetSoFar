using UnityEngine;
using System.Collections;

public class                        CharacterController : MonoBehaviour 
{
    public float                    _speed;                                 // speed of the character
    public float                    _jumpSpeed;                             // jumping intensity
    
    private bool                    _grounded = false;                      // jump
    private bool                    _doubleJump = false;                    // i don't think it needs any explaination;
    private bool                    _climbing = false;                      // climb
    private bool                    _lookLeft = false;                      // is the character looking left
    
    private float                   _gravityScale;                          // since it is set to 0 when we are on a ladder, we must remember it to reset it correctly when we leave the ladder

    private Animator                _anim;                                  // animator
    private Transform               _t;                                     // transform
    private Rigidbody2D             _rb2d;                                  // rigidbody2d

	void Start () 
    {
        _gravityScale = rigidbody2D.gravityScale;
        _anim = GetComponent<Animator>();
        _t = GetComponent<Transform>();
        _rb2d = GetComponent<Rigidbody2D>();
    }
	
	void Update () 
    {
        float actualSpeed = (_climbing && !_grounded) ? _speed / 5 : _speed;
        float HAxis = Input.GetAxis("Horizontal");
        float VAxis = Input.GetAxis("Vertical");

        Vector2 newVelocity = new Vector2(0, rigidbody2D.velocity.y);


        if (HAxis == 0)
            _anim.SetBool("isWalking", false);
        else
            _anim.SetBool("isWalking", true);

        if (HAxis > 0)
        {
            if (_lookLeft == true)
                _t.localScale = new Vector3(1, 1, 1);
            _lookLeft = false;
            newVelocity.x = actualSpeed;
        }
        else if (HAxis < 0)
        {
            if (_lookLeft == false)
                _t.localScale = new Vector3(-1, 1, 1);
            _lookLeft = true;
            newVelocity.x = -actualSpeed;
        }
        else if (_climbing)
            newVelocity = Vector2.zero;

        if (_climbing)
            if (VAxis > 0)
                newVelocity.y = _jumpSpeed;
            else if (VAxis < 0)
                newVelocity.y = -_jumpSpeed;
            else
                newVelocity.y = 0;

        if ((_grounded || _doubleJump || _climbing) && Input.GetAxis("Jump") > 0)
        {
            _grounded = false;
            _doubleJump = false;
            newVelocity.y = _jumpSpeed;
        }

        rigidbody2D.velocity = newVelocity;

        _anim.SetFloat("verticalVelocity", _rb2d.velocity.y);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            endJump();
        else if (col.gameObject.tag == "Moveable")
            col.gameObject.rigidbody2D.velocity = new Vector2(_speed / 5, col.gameObject.rigidbody2D.velocity.y);
        else if (col.gameObject.tag == "SpringBoard")
            startSpringBoard();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            _grounded = false;
            _anim.SetBool("isGrounded", false);
        }
        if (col.gameObject.tag == "Moveable")
            col.gameObject.rigidbody2D.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ladder" && _rb2d.velocity.y <= 0)
            startClimbingMode();
        else if (col.tag == "BonusDoubleJump")
            getDoubleJump(col);
    }
     

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ladder")
            endClimbingMode();
    }

    public void startJump()
    {
        _grounded = false;
        _doubleJump = false;
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpSpeed);
    }

    void endJump()
    {
        _grounded = true;
        _doubleJump = false;
        _anim.SetBool("isGrounded", true);
    }

    void startSpringBoard()
    {
        _grounded = false;
        rigidbody2D.velocity = new Vector2(0, _jumpSpeed * 2);
    }

    void startClimbingMode()
    {
        _climbing = true;
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = Vector2.zero;
    }

    void endClimbingMode()
    {
        _climbing = false;
        rigidbody2D.gravityScale = _gravityScale;
    }

    void getDoubleJump(Collider2D col)
    {
        _doubleJump = true;
        StartCoroutine("Coroutine_respawnItem", Instantiate(col.gameObject) as GameObject);
        Destroy(col.gameObject);
    }

    IEnumerator Coroutine_respawnItem(GameObject go)
    {
        go.SetActive(false);
        yield return new WaitForSeconds(10);
        go.SetActive(true);
    }
}
