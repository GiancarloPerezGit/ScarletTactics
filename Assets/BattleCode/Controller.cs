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
    private bool pause = true;
    public void Begin()
    {
        pause = false;
    }
    private void StatusCheck()
    {
        foreach(GameObject unit in units)
        {
            if(unit.GetComponent<Unit>().affected.Count > 0)
            {
                foreach (KeyValuePair<string, StatusEffect> effect in unit.GetComponent<Unit>().affected)
                {
                    if(effect.Value.duration > 0)
                    {
                        effect.Value.duration -= 1;
                    }
                    if( effect.Value.duration == 0 )
                    {
                        unit.GetComponent<Unit>().affected.Remove(effect.Key);
                        Destroy(effect.Value.gameObject);
                    }
                }
            }
        }
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
            if(unit.GetComponent<Unit>().alive)
            {
                if(unit.GetComponent<Stats>().AddCt())
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
        if (!pause)
        {
            if (!active)
            {
                StatusCheck();
            }
            else if (!waiting)
            {
                if (activeUnits.Count != 0)
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
}
