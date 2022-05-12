using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public GameObject activeUnit;
    public Controller clock;
    private bool playerControl = false;
    private int menuLayer = 0;
    private int currentOption = 0;
    public GameObject canvas;
    public GameObject baseUI;
    public GameObject moveConfirm;
    void Start()
    {
        
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
            playerControl = true;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControl)
        {
            if(menuLayer == 0)
            {

            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                activeUnit.GetComponent<Stats>().ct -= 80;
                clock.waiting = false;
                playerControl = false;
            }
        }
    }
}
