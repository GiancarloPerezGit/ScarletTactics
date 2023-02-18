using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMachine : MonoBehaviour
{
    public TileSelecting tileSelecting;
    public TileControls tileControls;
    public MenuControls menuControls;
    public TurnController turnController;

    public GameObject battleMenu;
    public MenuState startState;

    public int layer = 0;

    public GameObject tileCursor;
    public MapDict mapDict;
    public List<Tile> selectableTiles;
    public Tile selectedTile;
    public List<Tile> targetedTiles;
    public AttackInfo attackInfo;

    public List<GameObject> menuChain;

    public void Restart()
    {
        ResetChain();
        ResetCursor();
        startState.disabledStates.Clear();
        startState.currentIndex = 0;
        layer = 0;
        startState.Activate();
        battleMenu.SetActive(true);
        tileControls.enabled = false;
        menuControls.enabled = true;
    }

    public void Begin()
    {
        ChangeState(startState);
    }

    public void ChangeState(State state)
    {
        state.Activate();
    }

    public void MenuControl()
    {
        tileControls.enabled = false;
        menuControls.enabled = true;
    }

    public void TileControl()
    {
        tileControls.enabled = true;
        menuControls.enabled = false;
    }

    public void EnableChain()
    {
        foreach (GameObject menu in menuChain)
        {
            menu.SetActive(true);
        }
    }

    public void ResetChain()
    {
        foreach(GameObject menu in menuChain)
        {
            menu.SetActive(false);
        }
    }

    public void ResetCursor()
    {
        tileCursor.transform.position = turnController.activeUnit.GetComponent<Unit>().currentTile.transform.position + new Vector3(0, 0.5f, 0);
    }
}
