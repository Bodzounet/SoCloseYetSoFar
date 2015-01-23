using UnityEngine;
using System.Collections;

public class GenerateMapPrefab : MonoBehaviour {

    public GameObject item;

    public GameObject createdPrefab;

	void Start () 
    {
        GameObject go;

	    for (int i = 0; i < 10; i++)
        {
            go = Instantiate(item, Vector3.right * (i + 1), Quaternion.identity) as GameObject;
            go.transform.parent = createdPrefab.transform;
        }

        UnityEditor.PrefabUtility.CreatePrefab("Assets/prefab/testPrefab.prefab", createdPrefab, UnityEditor.ReplacePrefabOptions.Default);
	}

}
