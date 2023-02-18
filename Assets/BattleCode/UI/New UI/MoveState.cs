using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : TileState
{
    public override List<Tile> RangeFind()
    {
        return machine.turnController.activeUnit.GetComponent<Movement>().RangeFind();
    }
}
