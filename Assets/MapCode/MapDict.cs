using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDict : MonoBehaviour
{   
    public Dictionary<Vector3, Tile> mapGrid = new Dictionary<Vector3, Tile>();
    public Dictionary<Tile, List<(Tile,int)>> adjancencyList = new Dictionary<Tile, List<(Tile,int)>>();
    public Material selected;
    public Material selected2;
    public Material selected3;
    public Material selected4;
    public Material selected5;
    public bool skip = false;
    public Tile start;                                                                                                       
    public int mov;
    public int jmp;
    private int curMov;
    public Material unselected;
    public int weather;

    public Vector3 lineStart;
    public Vector3 lineEnd;

    private void Awake()
    {
        foreach (Tile t in gameObject.transform.GetComponentsInChildren<Tile>())
        {
            mapGrid.Add(t.transform.position, t.GetComponent<Tile>());
            t.GetComponent<Tile>().height = t.gameObject.transform.position.y;
        }
        foreach (KeyValuePair<Vector3, Tile> entry in mapGrid)
        {
            List<(Tile, int)> neighbors = new List<(Tile, int)>();
            for (float i = 0; i <= 30; i += 0.5f)
            {
                Vector3 west = new Vector3(entry.Key.x - 1, i, entry.Key.z);
                if (mapGrid.ContainsKey(west))
                {
                    neighbors.Add((mapGrid[west], mapGrid[west].moveCost));
                }
            }
            for (float i = 0; i <= 30; i += 0.5f)
            {
                Vector3 east = new Vector3(entry.Key.x + 1, i, entry.Key.z);
                if (mapGrid.ContainsKey(east))
                {
                    neighbors.Add((mapGrid[east], mapGrid[east].moveCost));
                }
            }
            for (float i = 0; i <= 30; i += 0.5f)
            {
                Vector3 south = new Vector3(entry.Key.x, i, entry.Key.z - 1);
                if (mapGrid.ContainsKey(south))
                {
                    neighbors.Add((mapGrid[south], mapGrid[south].moveCost));
                }
            }
            for (float i = 0; i <= 30; i += 0.5f)
            {
                Vector3 north = new Vector3(entry.Key.x, i, entry.Key.z + 1);
                if (mapGrid.ContainsKey(north))
                {
                    neighbors.Add((mapGrid[north], mapGrid[north].moveCost));
                }
            }
            adjancencyList.Add(entry.Value, neighbors);
        }
        foreach (KeyValuePair<Tile, List<(Tile, int)>> entry in adjancencyList)
        {
            string test = "Source: " + entry.Key.gameObject.name + " Neighbors: ";
            foreach ((Tile t, int i) in entry.Value)
            {
                test += t.gameObject.name + " ";
            }
        }

    }

    public void ResetTilePaint()
    {
        foreach (Transform t in gameObject.transform)
        {
            t.gameObject.GetComponent<Tile>().selected(unselected);
        }
    }

    public void ResetTileRoutes()
    {
        foreach (Transform t in gameObject.transform)
        {
            t.gameObject.GetComponent<Tile>().route = new List<Tile>();
        }
    }

    //public void pathfind()
    //{
    //    mapGrid.Clear();
    //    adjancencyList.Clear();
    //    foreach (Transform t in gameObject.transform)
    //    {
    //        mapGrid.Add(t.transform.position, t.GetComponent<Tile>());
    //        t.GetComponent<Tile>().height = t.gameObject.transform.position.y;
    //    }
    //    foreach (KeyValuePair<Vector3, Tile> entry in mapGrid)
    //    {
    //        List<(Tile, int)> neighbors = new List<(Tile, int)>();
    //        for (float i = 0; i <= 30; i += 0.5f)
    //        {
    //            Vector3 west = new Vector3(entry.Key.x - 1, i, entry.Key.z);
    //            if (mapGrid.ContainsKey(west))
    //            {
    //                if(entry.Key.y > mapGrid[west].gameObject.transform.position.y)
    //                {
    //                    if (!CheckReachability(new Vector3(entry.Value.westEdge.transform.position.x, entry.Value.westEdge.transform.position.y + 1.5f, entry.Value.westEdge.transform.position.z), new Vector3(mapGrid[west].gameObject.transform.position.x, mapGrid[west].gameObject.transform.position.y + 1.5f, mapGrid[west].gameObject.transform.position.z)))
    //                    {
    //                        neighbors.Add((mapGrid[west], mapGrid[west].moveCost));
    //                    }
    //                }
    //                else if(!CheckReachability(new Vector3(entry.Key.x, entry.Key.y + 1.5f, entry.Key.z), new Vector3(mapGrid[west].eastEdge.gameObject.transform.position.x, mapGrid[west].eastEdge.gameObject.transform.position.y + 1.5f, mapGrid[west].eastEdge.gameObject.transform.position.z)))
    //                {
    //                    neighbors.Add((mapGrid[west], mapGrid[west].moveCost));
    //                }
    //            }
    //        }
    //        for (float i = 0; i <= 30; i += 0.5f)
    //        {
    //            Vector3 east = new Vector3(entry.Key.x + 1, i, entry.Key.z);
                
    //            if (mapGrid.ContainsKey(east))
    //            {
    //                if (entry.Key.y > mapGrid[east].gameObject.transform.position.y)
    //                {
    //                    if (!CheckReachability(new Vector3(entry.Value.eastEdge.transform.position.x, entry.Value.eastEdge.transform.position.y + 1.5f, entry.Value.eastEdge.transform.position.z), new Vector3(mapGrid[east].gameObject.transform.position.x, mapGrid[east].gameObject.transform.position.y + 1.5f, mapGrid[east].gameObject.transform.position.z)))
    //                    {
    //                        neighbors.Add((mapGrid[east], mapGrid[east].moveCost));
    //                    }
    //                }
    //                else if (!CheckReachability(new Vector3(entry.Key.x, entry.Key.y + 1.5f, entry.Key.z), new Vector3(mapGrid[east].westEdge.gameObject.transform.position.x, mapGrid[east].westEdge.gameObject.transform.position.y + 1.5f, mapGrid[east].westEdge.gameObject.transform.position.z)))
    //                {
    //                    neighbors.Add((mapGrid[east], mapGrid[east].moveCost));
    //                }
    //            }
    //        }
    //        for (float i = 0; i <= 30; i += 0.5f)
    //        {
    //            Vector3 south = new Vector3(entry.Key.x, i, entry.Key.z - 1);
                
    //            if (mapGrid.ContainsKey(south))
    //            {
    //                if (entry.Key.y > mapGrid[south].gameObject.transform.position.y)
    //                {
    //                    if (!CheckReachability(new Vector3(entry.Value.southEdge.transform.position.x, entry.Value.southEdge.transform.position.y + 1.5f, entry.Value.southEdge.transform.position.z), new Vector3(mapGrid[south].gameObject.transform.position.x, mapGrid[south].gameObject.transform.position.y + 1.5f, mapGrid[south].gameObject.transform.position.z)))
    //                    {
    //                        neighbors.Add((mapGrid[south], mapGrid[south].moveCost));
    //                    }
    //                }
    //                else if (!CheckReachability(new Vector3(entry.Key.x, entry.Key.y + 1.5f, entry.Key.z), new Vector3(mapGrid[south].northEdge.gameObject.transform.position.x, mapGrid[south].northEdge.gameObject.transform.position.y + 1.5f, mapGrid[south].northEdge.gameObject.transform.position.z)))
    //                {
    //                    neighbors.Add((mapGrid[south], mapGrid[south].moveCost));
    //                }
    //            }
    //        }
    //        for (float i = 0; i <= 30; i += 0.5f)
    //        {
    //            Vector3 north = new Vector3(entry.Key.x, i, entry.Key.z + 1);
    //            if (mapGrid.ContainsKey(north))
    //            {
    //                if (entry.Key.y > mapGrid[north].gameObject.transform.position.y)
    //                {
    //                    if (!CheckReachability(new Vector3(entry.Value.northEdge.transform.position.x, entry.Value.northEdge.transform.position.y + 1.5f, entry.Value.northEdge.transform.position.z), new Vector3(mapGrid[north].gameObject.transform.position.x, mapGrid[north].gameObject.transform.position.y + 1.5f, mapGrid[north].gameObject.transform.position.z)))
    //                    {
    //                        neighbors.Add((mapGrid[north], mapGrid[north].moveCost));
    //                    }
    //                }
                    
    //                else if (!CheckReachability(new Vector3(entry.Key.x, entry.Key.y + 1.5f, entry.Key.z), new Vector3(mapGrid[north].southEdge.gameObject.transform.position.x, mapGrid[north].southEdge.gameObject.transform.position.y + 1.5f, mapGrid[north].southEdge.gameObject.transform.position.z)))
    //                {
    //                    neighbors.Add((mapGrid[north], mapGrid[north].moveCost));
    //                }
    //            }
    //        }
    //        adjancencyList.Add(entry.Value, neighbors);
    //    }
    //    foreach (KeyValuePair<Tile, List<(Tile, int)>> entry in adjancencyList)
    //    {
    //        string test = "Source: " + entry.Key.gameObject.name + " Neighbors: ";
    //        foreach ((Tile t, int i) in entry.Value)
    //        {
    //            test += t.gameObject.name + " ";
    //        }
    //        print(test);
    //    }
        
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
