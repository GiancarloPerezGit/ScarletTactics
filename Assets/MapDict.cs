using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDict : MonoBehaviour
{
    public Dictionary<Vector3, Tile> mapGrid = new Dictionary<Vector3, Tile>();
    public Dictionary<Tile, List<Tile>> adjancencyList = new Dictionary<Tile, List<Tile>>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in gameObject.transform)
        {
            mapGrid.Add(t.transform.position, t.GetComponent<Tile>());
        }
        foreach(KeyValuePair<Vector3, Tile> entry in mapGrid)
        {
            List<Tile> neighbors = new List<Tile>();
            for(float i = 0; i <= 20; i += 0.5f)
            {
                Vector3 west = new Vector3(entry.Key.x - 1, i, entry.Key.z);
                if(mapGrid.ContainsKey(west))
                {
                    neighbors.Add(mapGrid[west]);
                }
            }
            for (float i = 0; i <= 20; i += 0.5f)
            {
                Vector3 east = new Vector3(entry.Key.x + 1, i, entry.Key.z);
                if (mapGrid.ContainsKey(east))
                {
                    neighbors.Add(mapGrid[east]);
                }
            }
            for (float i = 0; i <= 20; i += 0.5f)
            {
                Vector3 south = new Vector3(entry.Key.x, i, entry.Key.z - 1);
                if (mapGrid.ContainsKey(south))
                {
                    neighbors.Add(mapGrid[south]);
                }
            }
            for (float i = 0; i <= 20; i += 0.5f)
            {
                Vector3 north = new Vector3(entry.Key.x, i, entry.Key.z + 1);
                if (mapGrid.ContainsKey(north))
                {
                    neighbors.Add(mapGrid[north]);
                }
            }
            adjancencyList.Add(entry.Value, neighbors);
        }
        foreach (KeyValuePair<Tile, List<Tile>> entry in adjancencyList)
        {
            string test = entry.Key.gameObject.name + " ";
            foreach (Tile t in entry.Value)
            {
                test += t.gameObject.name + " ";
            }
            print(test);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
