using SimpleJSON;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class                                GenerateMapPrefab : MonoBehaviour 
{
    public GameObject                       newPrefab;
    public TextAsset                        JSon;

    public GameObject[]                     prefabs;
    private Dictionary<string, GameObject>  dPrefabs;

	void Awake () 
    {
        initMap();

        var                 N = JSON.Parse(JSon.text);

        int                 height = N["height"].AsInt;
        int                 width = N["width"].AsInt;

        int                 id;
        int                 tmp;
        string              name = null;
        GameObject          go;
        
        Vector2             spawnPos = Vector2.zero;
        float               incrXY = prefabs[0].renderer.bounds.size.x;            // a bit dangerous, segF if no prefabs.count == 0 or no prefabs[0].renderer == null, but in both case, it is not supposed to.

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                id = N["layers"][0]["data"][i * width + j].AsInt;
                if (id != 0)
                {
                    for (int k = 0; k < N["tilesets"].Count; k++)
                    {
                        tmp = N["tilesets"][k]["firstgid"].AsInt;
                        if (tmp == id)
                            name = N["tilesets"][k]["name"];                        // also dangerous, if name is never assigned, but it's not supposed to occur if json file is OK.
                    }
                    go = Instantiate(dPrefabs[name], spawnPos, Quaternion.identity) as GameObject;
                    go.transform.parent = newPrefab.transform;
                }
                spawnPos.x += incrXY;
            }
            spawnPos.x = 0;
            spawnPos.y -= incrXY;
        }
        //UnityEditor.PrefabUtility.CreatePrefab("Assets/prefab/testPrefab.prefab", newPrefab, UnityEditor.ReplacePrefabOptions.Default);
	}

    void                                    initMap()
    {
        dPrefabs = new Dictionary<string, GameObject>();
        foreach (GameObject go in prefabs)
            dPrefabs.Add(go.name, go);
    }
}
