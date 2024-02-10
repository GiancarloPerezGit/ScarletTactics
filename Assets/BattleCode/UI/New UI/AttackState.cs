using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : TileState
{
    public override List<Tile> RangeFind()
    {
        GameObject weapon = machine.turnController.activeUnit.GetComponent<Unit>().mainHand.gameObject;
        return weapon.GetComponent<WeaponReach>().RangeFind();
    }
}
