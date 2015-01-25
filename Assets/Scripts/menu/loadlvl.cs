using UnityEngine;
using System.Collections;

public class loadlvl : MonoBehaviour 
{

    public string lvl;

    void OnMouseUp()
    {
        Application.LoadLevel(lvl);
    }
}
