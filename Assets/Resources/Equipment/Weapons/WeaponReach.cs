using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReach : RangeFinder
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
    //reachHoriz is the vertical tolerance of the weapon
    private (int, int) reachHoriz;
    //reachVert is the number of tiles weapon can reach
    private (float,float) reachVert;

    private Vector3 playerHeight = new Vector3(0, 1.5f, 0);

    public MapDict md;
   
    public TileSelecting ts;
    public bool animating = false;

    //Temporary materials to debug the pathfinding
    public Material selected;
    public Material selected2;
    public Material selected3;
    public Material selected4;
    public Material selected5;

    public GameObject player;

    private void Awake()
    {
        ts = FindObjectOfType<TileSelecting>();
        md = FindObjectOfType<MapDict>();
    }

    //Draws a line between 2 points, useful for checking if a tile can be jumped/walked to  
    public bool CheckReachability(Vector3 start, Vector3 end)
    {
        return Physics.Linecast(start, end);
    }


    public override List<Tile> RangeFind()
    {
        priorityQueue.Clear();
        reachableTiles.Clear();

        player = this.gameObject.transform.parent.gameObject; 
        start = player.GetComponent<Unit>().currentTile;
        reachHoriz = (player.GetComponent<Unit>().mainHand.horizRange.lower, player.GetComponent<Unit>().mainHand.horizRange.upper);
        reachVert = (player.GetComponent<Unit>().mainHand.vertTolerance.lower, player.GetComponent<Unit>().mainHand.vertTolerance.upper);

        priorityQueue.Enqueue((start, 0, start));
        reachableTiles = FindTiles(reachableTiles);
        print(reachableTiles.Count);
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
            else if (y.Value <= 4)
            {
                y.Key.selected(selected5);
            }
            tiles.Add(y.Key);
            //y.Key.gameObject.GetComponent<Renderer>().material = selected;
            print(y.Key.gameObject.name + " " + y.Value);

        }
        player.GetComponent<Stats>().controller.tc.selectableTiles = tiles;
        return tiles;
    }

    public Dictionary<Tile, int> FindTiles(Dictionary<Tile, int> reachableTiles)
    {
        (float,float) heightRange = (player.GetComponent<Unit>().height + player.GetComponent<Unit>().heightMod + reachVert.Item1, player.GetComponent<Unit>().height + player.GetComponent<Unit>().heightMod + reachVert.Item2);
        playerHeight.y = player.GetComponent<Unit>().height + player.GetComponent<Unit>().heightMod;
        for (int i = reachHoriz.Item1; i <= reachHoriz.Item2; i++)
        {
            if(i == 0)
            {

            }
            else
            {
                for (float k = 0; k <= 30; k += 0.5f)
                {
                    Vector3 possibleTile = new Vector3(start.transform.position.x + i, k, start.transform.position.z);

                    if (md.mapGrid.ContainsKey(possibleTile))
                    {
                        if (!CheckReachability(start.transform.position + playerHeight, possibleTile + playerHeight))
                        {
                            if (k >= heightRange.Item1 && k <= heightRange.Item2)
                            {
                                print("reached");
                                reachableTiles.Add(md.mapGrid[possibleTile], 1);
                            }
                            else if (md.mapGrid[possibleTile].unit != null)
                            {
                                float unitHeight = md.mapGrid[possibleTile].unit.GetComponent<Unit>().heightMod + md.mapGrid[possibleTile].unit.GetComponent<Unit>().height;
                                Vector3 unitTile = new Vector3(possibleTile.x, unitHeight, possibleTile.z);
                                if (!CheckReachability(start.transform.position + playerHeight, unitTile + playerHeight))
                                {
                                    if (unitHeight >= heightRange.Item1 && unitHeight <= heightRange.Item2)
                                    {
                                        reachableTiles.Add(md.mapGrid[possibleTile], 2);
                                    }
                                }
                            }
                        }
                        else if (md.mapGrid[possibleTile].unit != null)
                        {
                            float unitHeight = md.mapGrid[possibleTile].unit.GetComponent<Unit>().heightMod + md.mapGrid[possibleTile].unit.GetComponent<Unit>().height;
                            Vector3 unitTile = new Vector3(possibleTile.x, unitHeight, possibleTile.z);
                            if (!CheckReachability(start.transform.position + playerHeight, unitTile + playerHeight))
                            {
                                if (unitHeight >= heightRange.Item1 && unitHeight <= heightRange.Item2)
                                {
                                    reachableTiles.Add(md.mapGrid[possibleTile], 2);
                                }
                            }
                        }
                    }
                }
            }
            
        }
        //for (int i = reachHoriz.Item1; i <= reachHoriz.Item2; i++)
        //{
        //    for (float k = 0; k <= 30; k += 0.5f)
        //    {
        //        Vector3 possibleTile = new Vector3(start.transform.position.x - i, k, start.transform.position.z);
        //        if (md.mapGrid.ContainsKey(possibleTile))
        //        {
        //            if (!CheckReachability(start.transform.position + playerHeight, possibleTile + playerHeight))
        //            {
        //                if (k >= heightRange.Item1 && k <= heightRange.Item2)
        //                {
        //                    reachableTiles.Add(md.mapGrid[possibleTile], 1);
        //                }
        //                else if (md.mapGrid[possibleTile].unit != null)
        //                {
        //                    float unitHeight = md.mapGrid[possibleTile].unit.GetComponent<Unit>().heightMod + md.mapGrid[possibleTile].unit.GetComponent<Unit>().height;
        //                    Vector3 unitTile = new Vector3(possibleTile.x, unitHeight, possibleTile.z);
        //                    if (!CheckReachability(start.transform.position + playerHeight, unitTile + playerHeight))
        //                    {
        //                        if (unitHeight >= heightRange.Item1 && unitHeight <= heightRange.Item2)
        //                        {
        //                            reachableTiles.Add(md.mapGrid[possibleTile], 2);
        //                        }
        //                    }
        //                }
        //            }
        //            else if (md.mapGrid[possibleTile].unit != null)
        //            {
        //                float unitHeight = md.mapGrid[possibleTile].unit.GetComponent<Unit>().heightMod + md.mapGrid[possibleTile].unit.GetComponent<Unit>().height;
        //                Vector3 unitTile = new Vector3(possibleTile.x, unitHeight, possibleTile.z);
        //                if (!CheckReachability(start.transform.position + playerHeight, unitTile + playerHeight))
        //                {
        //                    if (unitHeight >= heightRange.Item1 && unitHeight <= heightRange.Item2)
        //                    {
        //                        reachableTiles.Add(md.mapGrid[possibleTile], 2);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        for (int i = reachHoriz.Item1; i <= reachHoriz.Item2; i++)
        {
            if(i == 0)
            {

            }
            else
            {
                for (float k = 0; k <= 30; k += 0.5f)
                {
                    Vector3 possibleTile = new Vector3(start.transform.position.x, k, start.transform.position.z + i);
                    if (md.mapGrid.ContainsKey(possibleTile))
                    {
                        if (!CheckReachability(start.transform.position + playerHeight, possibleTile + playerHeight))
                        {
                            if (k >= heightRange.Item1 && k <= heightRange.Item2)
                            {
                                reachableTiles.Add(md.mapGrid[possibleTile], 1);
                            }
                            else if (md.mapGrid[possibleTile].unit != null)
                            {
                                float unitHeight = md.mapGrid[possibleTile].unit.GetComponent<Unit>().heightMod + md.mapGrid[possibleTile].unit.GetComponent<Unit>().height;
                                Vector3 unitTile = new Vector3(possibleTile.x, unitHeight, possibleTile.z);
                                if (!CheckReachability(start.transform.position + playerHeight, unitTile + playerHeight))
                                {
                                    if (unitHeight >= heightRange.Item1 && unitHeight <= heightRange.Item2)
                                    {
                                        reachableTiles.Add(md.mapGrid[possibleTile], 2);
                                    }
                                }
                            }
                        }
                        else if (md.mapGrid[possibleTile].unit != null)
                        {
                            float unitHeight = md.mapGrid[possibleTile].unit.GetComponent<Unit>().heightMod + md.mapGrid[possibleTile].unit.GetComponent<Unit>().height;
                            Vector3 unitTile = new Vector3(possibleTile.x, unitHeight, possibleTile.z);
                            if (!CheckReachability(start.transform.position + playerHeight, unitTile + playerHeight))
                            {
                                if (unitHeight >= heightRange.Item1 && unitHeight <= heightRange.Item2)
                                {
                                    reachableTiles.Add(md.mapGrid[possibleTile], 2);
                                }
                            }
                        }
                    }
                }
            }
            
        }
        //for (int i = reachHoriz.Item1; i <= reachHoriz.Item2; i++)
        //{
        //    for (float k = 0; k <= 30; k += 0.5f)
        //    {
        //        Vector3 possibleTile = new Vector3(start.transform.position.x, k, start.transform.position.z - i);
        //        if (md.mapGrid.ContainsKey(possibleTile))
        //        {
        //            if (!CheckReachability(start.transform.position + playerHeight, possibleTile + playerHeight))
        //            {
        //                if (k >= heightRange.Item1 && k <= heightRange.Item2)
        //                {
        //                    reachableTiles.Add(md.mapGrid[possibleTile], 1);
        //                }
        //                else if (md.mapGrid[possibleTile].unit != null)
        //                {
        //                    float unitHeight = md.mapGrid[possibleTile].unit.GetComponent<Unit>().heightMod + md.mapGrid[possibleTile].unit.GetComponent<Unit>().height;
        //                    Vector3 unitTile = new Vector3(possibleTile.x, unitHeight, possibleTile.z);
        //                    if (!CheckReachability(start.transform.position + playerHeight, unitTile + playerHeight))
        //                    {
        //                        if (unitHeight >= heightRange.Item1 && unitHeight <= heightRange.Item2)
        //                        {
        //                            reachableTiles.Add(md.mapGrid[possibleTile], 2);
        //                        }
        //                    }
        //                }
        //            }
        //            else if (md.mapGrid[possibleTile].unit != null)
        //            {
        //                float unitHeight = md.mapGrid[possibleTile].unit.GetComponent<Unit>().heightMod + md.mapGrid[possibleTile].unit.GetComponent<Unit>().height;
        //                Vector3 unitTile = new Vector3(possibleTile.x, unitHeight, possibleTile.z);
        //                if (!CheckReachability(start.transform.position + playerHeight, unitTile + playerHeight))
        //                {
        //                    if (unitHeight >= heightRange.Item1 && unitHeight <= heightRange.Item2)
        //                    {
        //                        reachableTiles.Add(md.mapGrid[possibleTile], 2);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        return reachableTiles;
    }

    public void StartListening()
    {
        //ts.TileSelectEvent += Animate;
    }

    public void StopListening()
    {
        //ts.TileSelectEvent -= Animate;
    }

    public void Animate(Tile tile)
    {
        if(AttackEvent != null)
        {
            AttackEvent();
        }
    }

    public delegate void AttackDelegate();

    public event AttackDelegate AttackEvent;

    private void Update()
    {
        if (animating)
        {
            

        }
    }
}
