using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    //Pathfinding data structures
    //priorityQueue is just a regular queue currently. It holds all of the tiles that will be checked next;
    public Queue<(Tile, int, Tile)> priorityQueue = new Queue<(Tile, int, Tile)>();
    //reachableTiles is a dictionary that will hold the tiles that can be reached by the unit. The Value is an integer that states how many movements it
    //takes to reach the Key tile. A value of 0 means the Key tile is the start tile.
    private Dictionary<Tile, int> reachableTiles = new Dictionary<Tile, int>();

    //Unit variables
    //These variables are taken from the Unit that is doing the movement
    //start is the Tile the unit is on top of
    private Tile start;
    //jmp is the vertical tolerance of the units movement
    private int jmp;
    //mov is the number of tiles the unit can walk
    private int mov;

    private Vector3 playerHeight = new Vector3(0, 1.5f, 0);

    //A variable used to skip a tile if it is impassable by the unit.
    private bool skip;
    
    //The list of all tiles and its neighbors. Taken from the mapDict script of the battlefield.
    public Dictionary<Tile, List<(Tile, int)>> adjancencyList;

    public TileSelecting ts;
    public bool animating = false;
    public List<Tile> route;

    //Temporary materials to debug the pathfinding
    public Material selected;
    public Material selected2;
    public Material selected3;
    public Material selected4;
    public Material selected5;

    private void Awake()
    {
        ts = FindObjectOfType<TileSelecting>();
    }

    //Draws a line between 2 points, useful for checking if a tile can be jumped/walked to  
    public bool CheckReachability(Vector3 start, Vector3 end)
    {
        return Physics.Linecast(start, end);
    }

    public delegate void TileDelegate(List<Tile> tiles);

    public event TileDelegate TilesSelectedEvent;

    public void pathfind()
    {
        priorityQueue.Clear();
        reachableTiles.Clear();

        adjancencyList = GameObject.Find("Map").GetComponent<MapDict>().adjancencyList;
        start = this.gameObject.GetComponent<Stats>().currentTile;
        mov = this.gameObject.GetComponent<Stats>().mov;
        jmp = this.gameObject.GetComponent<Stats>().jmp;

        
        priorityQueue.Enqueue((start, 0, start));
        while (priorityQueue.Count > 0)
        {
            (Tile, int, Tile) subject = priorityQueue.Dequeue();
            //print("Currently testing: " + subject.Item1.gameObject.name + "with a cost of " + subject.Item2);
            Vector2 start = new Vector2(subject.Item3.transform.position.x, subject.Item3.transform.position.z);
            Vector2 end = new Vector2(subject.Item1.transform.position.x, subject.Item1.transform.position.z);
            Vector2 direction = (end - start);
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    if (subject.Item1.westEdge.impassable)
                    {
                        skip = true;
                    }
                    else if(subject.Item3.gameObject.transform.position.y > subject.Item1.gameObject.transform.position.y)
                    {
                        if(CheckReachability(new Vector3(subject.Item3.eastEdge.transform.position.x, subject.Item3.eastEdge.transform.position.y + 1.5f, subject.Item3.eastEdge.transform.position.z), subject.Item1.transform.position + playerHeight))
                        {
                            skip = true;
                        }
                    }
                    else
                    {
                        if (CheckReachability(subject.Item3.transform.position + playerHeight, subject.Item1.westEdge.transform.position + playerHeight))
                        {
                            skip = true;
                        }
                    }
                }
                else
                {
                    if (subject.Item1.eastEdge.impassable)
                    {
                        skip = true;
                    }
                    else if (subject.Item3.gameObject.transform.position.y > subject.Item1.gameObject.transform.position.y)
                    {
                        if (CheckReachability(subject.Item3.westEdge.transform.position + playerHeight, subject.Item1.transform.position + playerHeight))
                        {
                            skip = true;
                        }
                    }
                    else
                    {
                        if (CheckReachability(subject.Item3.transform.position + playerHeight, subject.Item1.eastEdge.transform.position + playerHeight))
                        {
                            skip = true;
                        }
                    }
                }
            }
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if (direction.y < 0)
                {
                    if (subject.Item1.northEdge.impassable)
                    {
                        skip = true;
                    }
                    else if (subject.Item3.gameObject.transform.position.y > subject.Item1.gameObject.transform.position.y)
                    {
                        if (CheckReachability(subject.Item3.southEdge.transform.position + playerHeight, subject.Item1.transform.position + playerHeight))
                        {
                            skip = true;
                        }
                    }
                    else
                    {
                        if (CheckReachability(subject.Item3.transform.position + playerHeight, subject.Item1.northEdge.transform.position + playerHeight))
                        {
                            skip = true;
                        }
                    }
                }
                else
                {
                    if (subject.Item1.southEdge.impassable)
                    {
                        skip = true;
                    }
                    else if (subject.Item3.gameObject.transform.position.y > subject.Item1.gameObject.transform.position.y)
                    {
                        if (CheckReachability(subject.Item3.northEdge.transform.position + playerHeight, subject.Item1.transform.position + playerHeight))
                        {
                            skip = true;
                        }
                    }
                    else
                    {
                        if (CheckReachability(subject.Item3.transform.position + playerHeight, subject.Item1.southEdge.transform.position + playerHeight))
                        {
                            skip = true;
                        }
                    }
                }
            }
            if (reachableTiles.ContainsKey(subject.Item1) && !skip)
            {
                if (reachableTiles[subject.Item1] > subject.Item2)
                {
                    reachableTiles[subject.Item1] = subject.Item2;
                    subject.Item1.route = new List<Tile>(subject.Item3.route);
                    subject.Item1.route.Add(subject.Item3);
                    foreach ((Tile t, int i) in adjancencyList[subject.Item1])
                    {

                        if (Mathf.Abs(t.height - subject.Item1.height) <= jmp && (subject.Item2 + t.moveCost) <= mov)
                        {
                            priorityQueue.Enqueue((t, subject.Item2 + t.moveCost, subject.Item1));
                        }

                    }
                }
                else
                {

                }
            }
            else if (!skip)
            {
                reachableTiles.Add(subject.Item1, subject.Item2);
                if(subject.Item1 == subject.Item3)
                {

                }
                else
                {
                    subject.Item1.route = new List<Tile>(subject.Item3.route);
                    subject.Item1.route.Add(subject.Item3);
                }
                
                foreach ((Tile t, int i) in adjancencyList[subject.Item1])
                {
                    if (Mathf.Abs(t.height - subject.Item1.height) <= jmp && (subject.Item2 + t.moveCost) <= mov)
                    {
                        priorityQueue.Enqueue((t, subject.Item2 + t.moveCost, subject.Item1));
                    }
                }
            }
            skip = false;
        }
        List<Tile> tiles = new List<Tile>();
        foreach (KeyValuePair<Tile, int> y in reachableTiles)
        {
            if (y.Value == 0)
            {
                y.Key.selected(selected);
            }
            else if (y.Value == 1)
            {
                y.Key.selected(selected2);
            }
            else if (y.Value == 2)
            {
                y.Key.selected(selected3);
            }
            else if (y.Value == 3)
            {
                y.Key.selected(selected4);
            }
            else if (y.Value == 4)
            {
                y.Key.selected(selected5);
            }
            tiles.Add(y.Key);
            //y.Key.gameObject.GetComponent<Renderer>().material = selected;
            //print(y.Key.gameObject.name + " " + y.Value);

        }
        gameObject.GetComponent<Stats>().controller.tc.selectableTiles = tiles;
    }

    public void StartListening()
    {
        ts.TileSelectEvent += Animate;
    }

    public void StopListening()
    {
        ts.TileSelectEvent -= Animate;
    }    

    public void Animate(Tile tile)
    {
        ts.TileSelectEvent -= Animate;
        route = tile.route;
        route.Add(tile);
        animating = true;
    }

    public delegate void MovementDelegate();

    public event MovementDelegate MovementEvent;

    private void Update()
    {
        if(animating)
        {
            if(route.Count != 0)
            {
                Vector3 truePosition = new Vector3(route[0].transform.position.x, route[0].transform.position.y + 0.5f, route[0].transform.position.z);
                transform.position = Vector3.Lerp(transform.position, truePosition, 0.05f);
                if(Vector3.Distance(transform.position, truePosition) <= 0.01)
                {
                    GetComponent<Stats>().currentTile.unit = null;
                    route[0].unit = this.gameObject;
                    GetComponent<Stats>().currentTile = route[0];
                    route.Remove(route[0]);
                }
            }
            else
            {
                animating = false;
                if(MovementEvent != null)
                {
                    MovementEvent();
                }
            }
            
        }
    }
}
