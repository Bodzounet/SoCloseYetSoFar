using UnityEngine;
using System.Collections;

public class endTuto : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) 
    {
        StartCoroutine("endLvl");
    }

    IEnumerator endLvl()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel("Lvl1");
    }
}
