using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public TurnController tc;
    public List<GameObject> units = new List<GameObject>();
    public Queue<GameObject> activeUnits = new Queue<GameObject>();
    public int clockTicks = 0;
    public bool active = false;
    public bool waiting = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            units.Add(enemy);
        }
        foreach (GameObject ally in GameObject.FindGameObjectsWithTag("Ally"))
        {
            units.Add(ally);
        }
    }

    private void StatusCheck()
    {
        clockTicks += 1;
        SlowCharge();
    }
    private void SlowCharge()
    {
        SlowResolution();
    }
    private void SlowResolution()
    {
        CTCharge();
    }
    private void CTCharge()
    {
        foreach(GameObject unit in units)
        {
            if(unit.GetComponent<Stats>().alive)
            {
                if(unit.GetComponent<Stats>().addCt())
                {
                    activeUnits.Enqueue(unit);
                }
            }
        }
        if(activeUnits.Count != 0)
        {
            active = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            StatusCheck();
        }
        else if(!waiting)
        {
            if(activeUnits.Count != 0)
            {
                tc.activeUnit = activeUnits.Dequeue();
                waiting = true;
                tc.StartTurn();
            }
            else
            {
                active = false;
            }
        }
        
    }
}
