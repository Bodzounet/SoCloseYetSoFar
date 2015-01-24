using UnityEngine;
using System.Collections;

public class            killMob : MonoBehaviour 
{
  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "Player")
    {
      if (transform.position.y < col.gameObject.transform.position.y)
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
    yield return new WaitForSeconds(2);
    Application.LoadLevel(Application.loadedLevelName);
  }
}
