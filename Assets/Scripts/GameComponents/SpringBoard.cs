using UnityEngine;
using System.Collections;

public class SpringBoard : MonoBehaviour 
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float sizeX = GetComponent<Renderer>().bounds.size.x;
        float sizeY = GetComponent<Renderer>().bounds.size.x;

        if (col.gameObject.tag == "Player" && 
            Physics2D.Linecast(transform.position + new Vector3(-sizeX, sizeY * 1.1f, 0), transform.position + new Vector3(sizeX, sizeY * 1.1f, 0)))
        {
            col.gameObject.GetComponent<CharacterController>().startSpringBoard();
            anim.Play("SpringBoard");
        }
    }
}
