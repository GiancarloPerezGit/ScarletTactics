using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepAttack : Attack
{
    public int charge;
    public Weapon wep;

    private void Start()
    {
        element = wep.ele;

    }


    public override List<Tile> RangeFind()
    {
        return wep.GetComponent<WeaponReach>().RangeFind();
    }

    public override List<Stats> HitTargets()
    {
        unitStats = machine.turnController.activeUnit.GetComponent<Stats>();
        foreach (Tile tile in machine.targetedTiles)
        {
            if (tile.unit != null)
            {
                if (!ignoreAllies)
                {
                    targetsStats.Add(tile.unit.GetComponent<Stats>());
                }
                else if (tile.unit.GetComponent<Stats>().faction != unitStats.faction)
                {
                    targetsStats.Add(tile.unit.GetComponent<Stats>());
                }
            }
        }
        foreach (Stats stats in targetsStats)
        {
            int facing = FlankBonus(unitStats.gameObject, stats.gameObject);
            int hitChance = 0;
            if (facing == 0)
            {
                hitChance = ((100 - stats.pCEv) * (100 - stats.pSEv) * (100 - stats.pAEv) * (100 - stats.wEv)) / 1000000;
            }
            else if (facing == 1)
            {
                hitChance = ((100 - stats.pSEv) * (100 - stats.pAEv) * (100 - stats.wEv)) / 10000;
            }
            else if (facing == 2)
            {
                hitChance = (100 - stats.pAEv) / 100;
            }
            print("Hitchance");
            if (Random.Range(1, 101) <= hitChance)
            {
                print("Hit");
            }
            else
            {
                targetsStats.Remove(stats);
                print("Miss");
            }
        }
        return targetsStats; 
    }

    public override void ApplyEffects()
    {
        unitStats.Attacking(tags);
        foreach (Stats stats in targetsStats)
        {
            stats.damageModifier = unitStats.damageModifier;
            stats.Defending(tags, unitStats);
            int xa = unitStats.gameObject.GetComponent<Unit>().mainHand.XAValue(stats.damageModifier, true);
            unitStats.gameObject.GetComponent<Unit>().mainHand.Damage(xa, charge, stats.gameObject);
        }
    }

    public int FlankBonus(GameObject attacker, GameObject defender)
    {
        Vector3 attackerTile = attacker.GetComponent<Unit>().currentTile.transform.position;
        Vector3 defenderTile = defender.GetComponent<Unit>().currentTile.transform.position;
        float xDiff = attackerTile.x - defenderTile.x;
        float zDiff = attackerTile.z - defenderTile.z;

        if (defender.GetComponent<Stats>().facing == 0)
        {
            if (Mathf.Abs(zDiff) > Mathf.Abs(xDiff))
            {
                if (zDiff > 0)
                {
                    return 0;
                }
                else if (zDiff < 0)
                {
                    return 2;
                }
            }
            else if (Mathf.Abs(zDiff) == Mathf.Abs(xDiff))
            {
                if (zDiff > 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        else if (defender.GetComponent<Stats>().facing == 1)
        {
            if (Mathf.Abs(xDiff) > Mathf.Abs(zDiff))
            {
                if (xDiff > 0)
                {
                    return 0;
                }
                else if (xDiff < 0)
                {
                    return 2;
                }
            }
            else if (Mathf.Abs(zDiff) == Mathf.Abs(xDiff))
            {
                if (xDiff > 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        else if (defender.GetComponent<Stats>().facing == 2)
        {
            if (Mathf.Abs(zDiff) > Mathf.Abs(xDiff))
            {
                if (zDiff < 0)
                {
                    return 0;
                }
                else if (zDiff > 0)
                {
                    return 2;
                }
            }
            else if (Mathf.Abs(zDiff) == Mathf.Abs(xDiff))
            {
                if (zDiff < 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        else if (defender.GetComponent<Stats>().facing == 3)
        {
            if (Mathf.Abs(xDiff) > Mathf.Abs(zDiff))
            {
                if (xDiff < 0)
                {
                    return 0;
                }
                else if (xDiff > 0)
                {
                    return 2;
                }
            }
            else if (Mathf.Abs(zDiff) == Mathf.Abs(xDiff))
            {
                if (xDiff < 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        return 1;
    }


}
