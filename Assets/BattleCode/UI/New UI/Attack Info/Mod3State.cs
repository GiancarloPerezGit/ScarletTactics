using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod3State : State
{
    public string[] tags;
    public bool friendlyFire;

    private Stats unitStats;
    private List<Stats> targetsStats;
    public override void Activate()
    {
        unitStats = machine.turnController.activeUnit.GetComponent<Stats>();
        //unitStats.aimModifier = unitStats.
        foreach(Tile tile in machine.targetedTiles)
        {
            if(tile.unit != null)
            {
                if(friendlyFire)
                {
                    targetsStats.Add(tile.unit.GetComponent<Stats>());
                }
                else if(tile.unit.GetComponent<Stats>().faction != unitStats.faction)
                {
                    targetsStats.Add(tile.unit.GetComponent<Stats>());
                }
            }
        }
        unitStats.Aimed(tags);
        foreach(Stats stats in targetsStats)
        {
            stats.Targeted(tags, unitStats);
        }
    }
}
