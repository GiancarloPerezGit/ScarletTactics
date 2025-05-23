using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnController : MonoBehaviour
{
    public InputMachine machine;
    public GameObject activeUnit;
    public Controller clock;
    private bool playerControl = false;
    public GameObject baseUI;

    public List<Tile> selectableTiles;

    public TestControls tc;
    public float hitChance;
    public Unit[] attackedUnits;
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
        //gameObject.GetComponent<MenuControls>().enabled = false;
        //gameObject.GetComponent<TileControls>().enabled = false;
        //gameObject.GetComponent<TileSelecting>().enabled = false;

    }

    //public delegate void EndTurnDelegate();

    //public event EndTurnDelegate EndTurnEvent;

    public void EndTurn()
    {
        if (playerControl)
        {
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
            machine.Restart();
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

    }
}
