using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoveState : State
{
    public bool animating;
    private GameObject unit;
    private List<Tile> route;

    public override void Activate()
    {
        machine.mapDict.ResetTilePaint();
        machine.tileCursor.SetActive(false);
        unit = machine.turnController.activeUnit;
        route = machine.selectedTile.route;
        route.Add(machine.selectedTile);
        animating = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (animating)
        {
            if (route.Count != 0)
            {
                Vector3 truePosition = new Vector3(route[0].transform.position.x, route[0].transform.position.y + 0.5f, route[0].transform.position.z);
                unit.transform.position = Vector3.Lerp(unit.transform.position, truePosition, 0.05f);

                if (Vector3.Distance(unit.transform.position, truePosition) <= 0.01)
                {
                    unit.GetComponent<Unit>().currentTile.unit = null;
                    route[0].unit = unit;
                    unit.GetComponent<Unit>().currentTile = route[0];
                    unit.GetComponent<Unit>().height = route[0].gameObject.transform.position.y;
                    route.Remove(route[0]);
                }
            }
            else
            {
                animating = false;
                machine.mapDict.ResetTileRoutes();
                machine.ResetChain();
                machine.startState.DisableState(0);
                machine.ResetCursor();
                machine.ChangeState(nextStates[0]);
            }
            
        }
    }
}
