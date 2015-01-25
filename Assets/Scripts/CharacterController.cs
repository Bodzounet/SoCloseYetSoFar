using UnityEngine;
using System.Collections;

public class                        CharacterController : MonoBehaviour 
{
    public float                    _speed;                                 // speed of the character
    public float                    _jumpSpeed;                             // jumping intensity
    public float                    _ladderSpeed;                           // ladder speed
    
    private bool                    _doubleJump = false;                    // i don't think it needs any explaination;
    private bool                    _climbing = false;                      // climb
    private bool                    _lookLeft = false;                      // is the character looking left

    private bool                    _isDead = false;

    
    private float                   _gravityScale;                          // since it is set to 0 when we are on a ladder, we must remember it to reset it correctly when we leave the ladder

    private Animator                _anim;                                  // animator
    private Transform               _t;                                     // transform
    private Rigidbody2D             _rb2d;                                  // rigidbody2d

    float                           _sizeX;
    float                           _sizeY;

    Vector2                         plateformVelocity;                      // if we are on a plateform, it's its velocity

	void Start () 
    {
        _sizeX = GetComponent<BoxCollider2D>().size.x;
        _sizeY = GetComponent<BoxCollider2D>().size.y;

        _gravityScale = rigidbody2D.gravityScale;
        _anim = GetComponent<Animator>();
        _t = GetComponent<Transform>();
        _rb2d = GetComponent<Rigidbody2D>();
    }
	
	void Update () 
    {
      if (_isDead == true)
        return;

        float actualSpeed = _speed;
        float HAxis = Input.GetAxis("Horizontal");
        float VAxis = Input.GetAxis("Vertical");

        Vector2 newVelocity = new Vector2(0, rigidbody2D.velocity.y);

        _climbing = isClimbing();

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
        else if (isOnMovingPlateform())
            newVelocity = plateformVelocity;

        if (_climbing)
            if (VAxis > 0)
                newVelocity.y = _ladderSpeed;
            else if (VAxis < 0)
                newVelocity.y = -_ladderSpeed;
            else
                newVelocity.y = 0;

        if (Input.GetButton("Jump"))
            newVelocity.y = jump(newVelocity.y);

        rigidbody2D.velocity = newVelocity;
        fixWallBug();

        _anim.SetFloat("verticalVelocity", _rb2d.velocity.y);
	}

    bool isClimbing()
    {
        int layer = 1 << LayerMask.NameToLayer("Player");
        layer = ~layer;

        RaycastHit2D hit1 = Physics2D.Linecast(transform.position + new Vector3(-_sizeX * 0.3f, _sizeY * 0.2f, 0), transform.position + new Vector3(_sizeX * 0.3f, _sizeY * 0.2f, 0), layer);
        RaycastHit2D hit2 = Physics2D.Linecast(transform.position + new Vector3(-_sizeX * 0.3f, _sizeY * 0.8f, 0), transform.position + new Vector3(_sizeX * 0.3f, _sizeY * 0.8f, 0), layer);

        if ((hit1 && hit1.transform.gameObject.tag == "Ladder") || (hit2 && hit2.transform.gameObject.tag == "Ladder"))
        {
            rigidbody2D.gravityScale = 0;
            _anim.SetBool("isOnLadder", true);
            return true;
        }
        rigidbody2D.gravityScale = _gravityScale;
        _anim.SetBool("isOnLadder", false);
        return false;
    }

    bool isOnMovingPlateform()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position + new Vector3(-_sizeX * 0.9f, -_sizeY * 0.1f, 0), transform.position + new Vector3(_sizeX * 0.9f, -_sizeY * 0.1f, 0));
        if (hit && hit.transform.gameObject.tag == "MovingPlateform")
        {
            plateformVelocity = hit.rigidbody.velocity;
            return true;
        }
        return false;
    }

    float jump(float currentVal)
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position + new Vector3(-_sizeX * 0.65f, -_sizeY * 0.1f, 0), transform.position + new Vector3(_sizeX * 0.65f, -_sizeY * 0.1f, 0));
        if ((hit && (hit.transform.gameObject.tag == "Ground" || hit.transform.gameObject.tag == "MovingPlateform")) || _doubleJump || _climbing)
        {
            _doubleJump = false;
            return _jumpSpeed;
        }
        return currentVal;
    }

    void fixWallBug()
    {
        float X = _sizeX;

        if (_lookLeft)
            X *= -1;

        if (Physics2D.Linecast(transform.position + new Vector3(X, 0, 0), transform.position + new Vector3(X, _sizeY, 0)))
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        if (Physics2D.Linecast(transform.position + new Vector3(-X * 0.65f, _sizeY * 1.1f, 0), transform.position + new Vector3(X * 0.65f, _sizeY * 1.1f, 0)))
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -1);
    }

    //void OnDrawGizmos()
    //{
    //    float sizeX = GetComponent<BoxCollider2D>().size.x;
    //    float sizeY = GetComponent<BoxCollider2D>().size.y;

    //    if (_lookLeft)
    //        sizeX *= -1;

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(transform.position + new Vector3(-sizeX * 0.65f, sizeY * 1.1f, 0), transform.position + new Vector3(sizeX * 0.65f, sizeY * 1.1f, 0));
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position + new Vector3(sizeX, 0, 0), transform.position + new Vector3(sizeX, sizeY, 0));
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position + new Vector3(-sizeX * 0.65f, -sizeY * 0.1f, 0), transform.position + new Vector3(sizeX * 0.65f, -sizeY * 0.1f, 0));
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position + new Vector3(-_sizeX * 0.3f, _sizeY * 0.2f, 0), transform.position + new Vector3(_sizeX * 0.3f, _sizeY * 0.2f, 0));
    //    Gizmos.DrawLine(transform.position + new Vector3(-_sizeX * 0.3f, _sizeY * 0.8f, 0), transform.position + new Vector3(_sizeX * 0.3f, _sizeY * 0.8f, 0));
    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "MovingPlateform")
            endJump();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            _anim.SetBool("isGrounded", false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
         if (col.tag == "BonusDoubleJump")
            getDoubleJump(col);
    }

    public void startJump()
    {
        _doubleJump = false;
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpSpeed);
    }

    void endJump()
    {
        _doubleJump = false;
        _anim.SetBool("isGrounded", true);
    }

    public void startSpringBoard()
    {
        rigidbody2D.velocity = new Vector2(0, _jumpSpeed * 1.5f);
    }

    void getDoubleJump(Collider2D col)
    {
        _doubleJump = true;
        StartCoroutine("Coroutine_respawnItem", Instantiate(col.gameObject) as GameObject);
        Destroy(col.gameObject);
    }

    public void dead()
    {
      _isDead = true;
      rigidbody2D.velocity = new Vector2(0, 0);
    }

    IEnumerator Coroutine_respawnItem(GameObject go)
    {
        go.SetActive(false);
        yield return new WaitForSeconds(10);
        go.SetActive(true);
    }
}
