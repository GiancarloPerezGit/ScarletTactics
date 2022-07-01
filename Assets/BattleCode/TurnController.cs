using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnController : MonoBehaviour
{
    public GameObject activeUnit;
    public Controller clock;
    private bool playerControl = false;
    private int menuLayer = 0;
    private int currentOption = 0;

    public GameObject baseUI;

    public List<Tile> selectableTiles;

    public TestControls tc;

    private bool endTurnVar = false;
    private int reduceCT = 60;

    //private void Awake()
    //{
    //    tc = new TestControls();
    //}

    //private void OnEnable()
    //{
    //    tc.Debugging.Endturn.started += endTurn;
    //    tc.Debugging.Endturn.Enable();
    //}

    //private void OnDisable()
    //{
    //    tc.Debugging.Endturn.started -= endTurn;
    //    tc.Debugging.Endturn.Disable();
    //}
    public void Start()
    {
        gameObject.GetComponent<MenuControls>().enabled = false;
        gameObject.GetComponent<TileControls>().enabled = false;
        gameObject.GetComponent<TileSelecting>().enabled = false;

    }

    public delegate void EndTurnDelegate();

    public event EndTurnDelegate EndTurnEvent;

    public void EndTurn()
    {
        if (playerControl)
        {
            activeUnit.GetComponent<Stats>().ct -= reduceCT;
            if (activeUnit.GetComponent<Stats>().ct > 60)
            {
                activeUnit.GetComponent<Stats>().ct = 60;
            }
            EndTurnEvent?.Invoke();
            activeUnit.GetComponent<Walking>().MovementEvent -= ReduceCT;
            playerControl = false;
        }
        clock.waiting = false;
    }

    public void StartTurn()
    {
        print(clock.clockTicks + " " + activeUnit.name);
        if(activeUnit.GetComponent<Stats>().faction != 0)
        {
            activeUnit.GetComponent<Stats>().ct -= 100;
            clock.waiting = false;
        }
        else
        {
            baseUI.SetActive(true);
            activeUnit.GetComponent<Walking>().MovementEvent += ReduceCT;
            playerControl = true;
            
        }
    }

    public void ReduceCT()
    {
        if(reduceCT == 80)
        {
            reduceCT = 100;
        }
        else if(reduceCT == 60)
        {
            reduceCT = 80;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(playerControl)
        //{
        //    if(menuLayer == 0)
        //    {

        //    }
        //    if(endTurnVar)
        //    {
        //        activeUnit.GetComponent<Stats>().ct -= 80;
        //        clock.waiting = false;
        //        playerControl = false;
        //        endTurnVar=false;
        //    }
        //}
    }
}
