using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawn : MonoBehaviour
{
    public GameObject spawn;
    public GameObject parent;
    public Vector3 start;
    public Vector3 end;

    private void Start()
    {
        Debug.DrawLine(start, end);
    }
    public void spawnOneHundred()
    {
        for(float i = 0; i < 10; i++)
        {
            for(float j = 0; j < 11; j++)
            {
                GameObject spawned = Instantiate(spawn, new Vector3(i, 0, j), Quaternion.identity, parent.transform);
                spawned.tag = "Spawned";
                spawned.name = spawned.transform.position.ToString();
            }
        }
    }

    public void drawLine()
    {
        
    }

   

    public void spawnBuilding()
    {
        for(float i = 0; i < 10; i++)
        {
            for(float k = 0; k < 10; k++)
            {
                GameObject spawned = Instantiate(spawn, new Vector3(i, 0, k), Quaternion.identity, parent.transform);
                spawned.tag = "Spawned";
                spawned.name = spawned.transform.position.ToString();
            }
        }
        for (float i = 0; i < 10; i++)
        {
            for (float k = 0; k < 10; k++)
            {
                GameObject spawned = Instantiate(spawn, new Vector3(i, 10, k), Quaternion.identity, parent.transform);
                spawned.GetComponent<Tile>().height = 2;
                spawned.tag = "Spawned";
                spawned.name = spawned.transform.position.ToString();
            }
        }
    }

    public void deleteAll()
    {
        foreach(GameObject i in GameObject.FindGameObjectsWithTag("Spawned"))
        {
            DestroyImmediate(i);
        }
    }
}
