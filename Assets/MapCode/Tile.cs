using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int moveCost = 1;
    public float height = 0;
    public bool tempSelect = false;
    public Edge northEdge;
    public Edge southEdge;
    public Edge eastEdge;
    public Edge westEdge;

    public GameObject unit;

    public List<Tile> route;
    void Start()
    {
        route = new List<Tile>();
    }

    public void selected(Material t)
    {
        if(tempSelect)
        {

        }
        else
        {
            gameObject.GetComponent<Renderer>().material = t;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
