using UnityEngine;
using System.Collections;

public class LockedDoor : MonoBehaviour {

    public GameObject key;
    public int lvl;

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel("lvl" + lvl);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (key.GetComponent<KeyBehaviour>()._isPickedUp)
        {
          col.GetComponent<Animator>().Play("wtf");
          StartCoroutine("LoadNextLevel");
        }
    }
}
