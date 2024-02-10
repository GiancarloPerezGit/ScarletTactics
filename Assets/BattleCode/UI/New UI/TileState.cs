using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileState : State
{
    public RangeFinder rangeFinder;

    private GameObject cursor;
    private MapDict mapDict;
    public List<Tile> selectableTiles;

    private Vector3 currentPosition;
    private Vector3 tileHeight = new Vector3(0, 0.5f, 0);

    public override void Activate()
    {
        machine.battleMenu.SetActive(false);
        machine.TileControl();
        machine.selectedTile = null;
        cursor = machine.tileCursor;
        currentPosition = cursor.transform.position - tileHeight;
        cursor.SetActive(true);
        mapDict = machine.mapDict;
        selectableTiles = RangeFind();
        machine.tileControls.UpEvent += MoveUp;
        machine.tileControls.RightEvent += MoveRight;
        machine.tileControls.DownEvent += MoveDown;
        machine.tileControls.LeftEvent += MoveLeft;
        machine.tileControls.AscendEvent += Ascend;
        machine.tileControls.DescendEvent += Descend;
        machine.tileControls.SelectEvent += Select;
        machine.tileControls.CancelEvent += Cancel;
    }

    public abstract List<Tile> RangeFind();
    
    public void UpdateInfo(Tile tile)
    {
        if(tile.unit != null)
        {
            machine.attackInfo.UpdateTargetInfo(tile.unit.GetComponent<Stats>());
        }
        else
        {
            machine.attackInfo.TurnOff();
        }
    }

    public void MoveUp()
    {
        Vector3 testPosition = currentPosition + Vector3.forward;
        Vector3 heightTest;
        Vector3 closestTile = Vector3.zero;
        float breakPoint = 30;
        for (float i = 0; i <= 30; i += 0.5f)
        {
            if (i == breakPoint + 0.5f)
            {
                cursor.transform.position = closestTile + tileHeight;
                currentPosition = closestTile;
                UpdateInfo(mapDict.mapGrid[currentPosition]);
                break;
            }
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (mapDict.mapGrid.ContainsKey(testPosition))
            {
                heightTest = testPosition - currentPosition;
                if (heightTest.y < 0)
                {
                    closestTile = testPosition;
                    breakPoint = Mathf.Abs(closestTile.y - currentPosition.y) + currentPosition.y;
                }
                else if (heightTest.y >= 0)
                {
                    cursor.transform.position = testPosition + tileHeight;
                    currentPosition = testPosition;
                    UpdateInfo(mapDict.mapGrid[currentPosition]);
                    break;
                }
            }
            else if (i == 30)
            {
                cursor.transform.position = currentPosition + tileHeight;
            }
        }
    }
    public void MoveRight()
    {
        Vector3 testPosition = currentPosition + Vector3.right;
        Vector3 heightTest;
        Vector3 closestTile = Vector3.zero;
        float breakPoint = 30;
        for (float i = 0; i <= 30; i += 0.5f)
        {
            if (i == breakPoint + 0.5f)
            {
                cursor.transform.position = closestTile + tileHeight;
                currentPosition = closestTile;
                UpdateInfo(mapDict.mapGrid[currentPosition]);
                break;
            }
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (mapDict.mapGrid.ContainsKey(testPosition))
            {
                heightTest = testPosition - currentPosition;
                if (heightTest.y < 0)
                {
                    closestTile = testPosition;
                    breakPoint = Mathf.Abs(closestTile.y - currentPosition.y) + currentPosition.y;
                }
                else if (heightTest.y >= 0)
                {
                    cursor.transform.position = testPosition + tileHeight;
                    currentPosition = testPosition;
                    UpdateInfo(mapDict.mapGrid[currentPosition]);
                    break;
                }
            }
            else if (i == 30)
            {
                cursor.transform.position = currentPosition + tileHeight;
            }
        }
    }
    public void MoveDown()
    {
        Vector3 testPosition = currentPosition + Vector3.back;
        Vector3 heightTest;
        Vector3 closestTile = Vector3.zero;
        float breakPoint = 30;
        for (float i = 0; i <= 30; i += 0.5f)
        {
            if (i == breakPoint + 0.5f)
            {
                cursor.transform.position = closestTile + tileHeight;
                currentPosition = closestTile;
                UpdateInfo(mapDict.mapGrid[currentPosition]);
                break;
            }
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (mapDict.mapGrid.ContainsKey(testPosition))
            {
                heightTest = testPosition - currentPosition;
                if (heightTest.y < 0)
                {
                    closestTile = testPosition;
                    breakPoint = Mathf.Abs(closestTile.y - currentPosition.y) + currentPosition.y;
                }
                else if (heightTest.y >= 0)
                {
                    cursor.transform.position = testPosition + tileHeight;
                    currentPosition = testPosition;
                    UpdateInfo(mapDict.mapGrid[currentPosition]);
                    break;
                }
            }
            else if (i == 30)
            {
                cursor.transform.position = currentPosition + tileHeight;
            }
        }
    }
    public void MoveLeft()
    {
        Vector3 testPosition = currentPosition + Vector3.left;
        Vector3 heightTest;
        Vector3 closestTile = Vector3.zero;
        float breakPoint = 30;
        for (float i = 0; i <= 30; i += 0.5f)
        {
            if (i == breakPoint + 0.5f)
            {
                cursor.transform.position = closestTile + tileHeight;
                currentPosition = closestTile;
                UpdateInfo(mapDict.mapGrid[currentPosition]);
                break;
            }
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (mapDict.mapGrid.ContainsKey(testPosition))
            {
                heightTest = testPosition - currentPosition;
                if (heightTest.y < 0)
                {
                    closestTile = testPosition;
                    breakPoint = Mathf.Abs(closestTile.y - currentPosition.y) + currentPosition.y;
                }
                else if (heightTest.y >= 0)
                {
                    cursor.transform.position = testPosition + tileHeight;
                    currentPosition = testPosition;
                    UpdateInfo(mapDict.mapGrid[currentPosition]);
                    break;
                }
            }
            else if (i == 30)
            {
                cursor.transform.position = currentPosition + tileHeight;
            }
        }
    }
    public void Ascend()
    {

        Vector3 testPosition = currentPosition;
        for (float i = currentPosition.y + 0.5f; i <= 30; i += 0.5f)
        {
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (mapDict.mapGrid.ContainsKey(testPosition))
            {
                cursor.transform.position = testPosition + tileHeight;
                currentPosition = testPosition;
                UpdateInfo(mapDict.mapGrid[currentPosition]);
                break;
            }
            else if (i == 30)
            {
                cursor.transform.position = currentPosition + tileHeight;
            }
        }
    }
    public void Descend()
    {
        Vector3 testPosition = currentPosition;
        for (float i = currentPosition.y - 0.5f; i >= 0; i -= 0.5f)
        {
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (mapDict.mapGrid.ContainsKey(testPosition))
            {
                cursor.transform.position = testPosition + tileHeight;
                currentPosition = testPosition;
                UpdateInfo(mapDict.mapGrid[currentPosition]);
                break;
            }
            else if (i == 0)
            {
                cursor.transform.position = currentPosition + tileHeight;
            }
        }
    }

    public void Select()
    {
        if (selectableTiles.Contains(mapDict.mapGrid[currentPosition]))
        {
            Disconnect();
            machine.selectedTile = mapDict.mapGrid[currentPosition];
            machine.ChangeState(nextStates[0]);
        }
    }

    public void Cancel()
    {
        if (previousState != null)
        {
            Disconnect();
            machine.mapDict.ResetTilePaint();
            machine.tileCursor.SetActive(false);
            machine.ResetCursor();
            machine.attackInfo.TurnOff();
            machine.ChangeState(previousState);
        }
    }

    public void Disconnect()
    {
        machine.tileControls.UpEvent -= MoveUp;
        machine.tileControls.RightEvent -= MoveRight;
        machine.tileControls.DownEvent -= MoveDown;
        machine.tileControls.LeftEvent -= MoveLeft;
        machine.tileControls.AscendEvent -= Ascend;
        machine.tileControls.DescendEvent -= Descend;
        machine.tileControls.SelectEvent -= Select;
        machine.tileControls.CancelEvent -= Cancel; 
    }

}
