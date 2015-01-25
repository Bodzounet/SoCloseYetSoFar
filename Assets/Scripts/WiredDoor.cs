using UnityEngine;
using System.Collections;

public class WiredDoor : MonoBehaviour {

    public GameObject wiredDoor;

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel("lvl3");
    }

    void    OnTriggerEnter2D(Collider2D col) {
        if (wiredDoor.activeSelf == false)
        {
            col.GetComponent<Animator>().Play("wtf");
            StartCoroutine("LoadNextLevel");
        }
    }
}
