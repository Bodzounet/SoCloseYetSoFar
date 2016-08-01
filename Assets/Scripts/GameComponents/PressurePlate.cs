using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {

    public GameObject[]     affectedBlock;
    public GameObject       wiredDoor;
	
    void Start () {
        for (int i = 0; i < affectedBlock.Length; i++)
            affectedBlock[i].SetActive(false);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        for (int i = 0; i < affectedBlock.Length; i++)
            affectedBlock[i].SetActive(true);
        if (wiredDoor != null)
            wiredDoor.SetActive(false);
    }
}
