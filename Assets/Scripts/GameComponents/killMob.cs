﻿using UnityEngine;
using System.Collections;

public class            killMob : MonoBehaviour 
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float x = renderer.bounds.size.x;
            float y = renderer.bounds.size.y;

            RaycastHit2D hit = Physics2D.Linecast(transform.position + new Vector3(-x * 0.8f, y, 0), transform.position + new Vector3(x * 0.8f, y, 0));
            if (hit && hit.transform.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<CharacterController>().startJump();
                Destroy(gameObject);
            }
            else
            {
                col.gameObject.GetComponent<Animator>().Play("death");
                col.gameObject.GetComponent<CharacterController>().dead();
                StartCoroutine("loadDeath");
            }
        }
    }

  IEnumerator loadDeath()
  {
    yield return new WaitForSeconds(1);
    Application.LoadLevel(Application.loadedLevelName);
  }
}
