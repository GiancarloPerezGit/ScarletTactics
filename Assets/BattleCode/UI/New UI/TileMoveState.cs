using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoveState : State
{
    public bool animating;
    private GameObject unit;
    private List<Tile> route;
    private Tile startTile;
    private bool facingSet = false;
    public override void Activate()
    {
        machine.mapDict.ResetTilePaint();
        machine.tileCursor.SetActive(false);
        unit = machine.turnController.activeUnit;
        startTile = unit.GetComponent<Unit>().currentTile;
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
                if (!facingSet)
                {
                    SetFacing();
                }
                Vector3 truePosition = new Vector3(route[0].transform.position.x, route[0].transform.position.y + 0.5f, route[0].transform.position.z);
                
                
                unit.transform.position = Vector3.Lerp(unit.transform.position, truePosition, 0.05f);

                if (Vector3.Distance(unit.transform.position, truePosition) <= 0.01)
                {
                    //SetFacing();
                    
                    unit.GetComponent<Unit>().currentTile.unit = null;
                    if (route[0].unit != null)
                    {

                    }
                    else
                    {
                        route[0].unit = unit;
                        unit.GetComponent<Unit>().currentTile = route[0];
                    }
                    
                    
                    startTile = route[0];
                    facingSet = false;
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

    public void SetFacing()
    {
        Vector3 startTilePos = startTile.transform.position;
        Vector3 endTilePos = route[0].transform.position;
        startTilePos.y = 0;
        endTilePos.y = 0;
        if (startTilePos - endTilePos == Vector3.back)
        {
            unit.GetComponent<Stats>().facing = 0;
            unit.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (startTilePos - endTilePos == Vector3.left)
        {
            unit.GetComponent<Stats>().facing = 1;
            unit.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (startTilePos - endTilePos == Vector3.forward)
        {
            unit.GetComponent<Stats>().facing = 2;
            unit.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (startTilePos - endTilePos == Vector3.right)
        {
            unit.GetComponent<Stats>().facing = 3;
            unit.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        facingSet = true;
    }
}
