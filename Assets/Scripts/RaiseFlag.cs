using UnityEngine;
using System.Collections;

public class RaiseFlag : MonoBehaviour {

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel("lvl2");
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player")
        {
            col.GetComponent<Animator>().Play("wtf");
            GetComponent<Animator>().Play("raise_flag");
            StartCoroutine("LoadNextLevel");
        }
    }
}
