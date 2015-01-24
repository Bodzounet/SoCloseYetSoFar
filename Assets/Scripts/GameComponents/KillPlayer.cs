using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{

  void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.tag == "Player")
    {
      col.gameObject.GetComponent<Animator>().Play("death");
      col.GetComponent<CharacterController>().dead();
      StartCoroutine("loadDeath");
    }
  }

  IEnumerator loadDeath()
  {
    yield return new WaitForSeconds(2);
    Application.LoadLevel("test");
  }
}
