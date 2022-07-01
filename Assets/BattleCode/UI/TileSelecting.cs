using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileSelecting : MonoBehaviour
{
    private TurnController tc;
    public TileControls controller;
    public GameObject cursor;
    public MapDict md;
    private List<Tile> tiles = new List<Tile>();
    private Vector3 currentPosition;

    private Vector3 tileHeight = new Vector3(0, 0.5f, 0);

    private MenuControls mc;
    private bool tileFound = false;
    // Start is called before the first frame update
    private void Awake()
    {
        tc = FindObjectOfType<TurnController>();
        controller = FindObjectOfType<TileControls>();
        mc = tc.GetComponent<MenuControls>();
    }

    private void OnEnable()
    {
        controller.enabled = true;
        tiles = tc.selectableTiles;
        controller.UpEvent += MoveUp;
        controller.RightEvent += MoveRight;
        controller.DownEvent += MoveDown;
        controller.LeftEvent += MoveLeft;
        controller.AscendEvent += Ascend;
        controller.DescendEvent += Descend;
        controller.SelectEvent += Select;
        currentPosition = tc.activeUnit.GetComponent<Stats>().currentTile.transform.position;
        cursor.transform.position = currentPosition + new Vector3(0,0.5f,0);
        cursor.SetActive(true);
    }

    private void OnDisable()
    {
        controller.UpEvent -= MoveUp;
        controller.RightEvent -= MoveRight;
        controller.DownEvent -= MoveDown;
        controller.LeftEvent -= MoveLeft;
        controller.AscendEvent -= Ascend;
        controller.DescendEvent -= Descend;
        controller.SelectEvent -= Select;
        controller.enabled = false;
        cursor.SetActive(false);
    }

    public void MoveUp()
    {
        Vector3 testPosition = currentPosition + Vector3.forward;
        Vector3 heightTest;
        Vector3 closestTile = Vector3.zero;
        float breakPoint = 30;
        for(float i = 0; i <= 30; i+=0.5f)
        {
            if(i == breakPoint + 0.5f)
            {
                cursor.transform.position = closestTile + tileHeight;
                currentPosition = closestTile;
                break;
            }
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (md.mapGrid.ContainsKey(testPosition))
            {
                heightTest = testPosition - currentPosition;
                if(heightTest.y < 0)
                {
                    closestTile = testPosition;
                    breakPoint = Mathf.Abs(closestTile.y - currentPosition.y) + currentPosition.y;
                }
                else if(heightTest.y >= 0)
                {
                    cursor.transform.position = testPosition + tileHeight;
                    currentPosition = testPosition;
                    break;
                }
            }
            else if(i == 30)
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
                break;
            }
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (md.mapGrid.ContainsKey(testPosition))
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
                break;
            }
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (md.mapGrid.ContainsKey(testPosition))
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
                break;
            }
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (md.mapGrid.ContainsKey(testPosition))
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
            if (md.mapGrid.ContainsKey(testPosition))
            {
                cursor.transform.position = testPosition + tileHeight;
                currentPosition = testPosition;
                break;
            }
            else if (i == 30)
            {
                cursor.transform.position = currentPosition + tileHeight;
                Debug.Log("Ascend");
            }
        }
    }
    public void Descend()
    {
        Vector3 testPosition = currentPosition;
        for (float i = currentPosition.y - 0.5f; i >= 0; i -= 0.5f)
        {
            testPosition = new Vector3(testPosition.x, i, testPosition.z);
            if (md.mapGrid.ContainsKey(testPosition))
            {
                cursor.transform.position = testPosition + tileHeight;
                currentPosition = testPosition;
                break;
            }
            else if (i == 0)
            {
                cursor.transform.position = currentPosition + tileHeight;
            }
        }
    }
    public delegate void TileSelectDelegate(Tile tile);

    public event TileSelectDelegate TileSelectEvent;

    public void Select()
    {
        if(tiles.Contains(md.mapGrid[currentPosition]))
        {
            if(TileSelectEvent != null)
            {
                TileSelectEvent(md.mapGrid[currentPosition]);
                controller.enabled = false;
                this.enabled = false;
            }
        }
    }

}
