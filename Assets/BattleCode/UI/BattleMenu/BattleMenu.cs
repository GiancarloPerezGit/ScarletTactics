using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleMenu : Menu
{
    public TurnController turnController;
    public int startIndex = 0;
    private int totalOptions;
    private int currentIndex = 0;
    private MenuControls controller;
    private GameObject cursor;
    private Vector3 cursorOffset = new Vector3(-115f, -12.5f, 0);
    private void Awake()
    {
        controller = FindObjectOfType<MenuControls>();
        turnController = FindObjectOfType<TurnController>();    
    }

    private void Start()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public delegate void BattleMenuDelegate();

    public event BattleMenuDelegate BattleMenuEvent;

    private void OnEnable()
    {
        if(!disabled)
        {
            //turnController.EndTurnEvent += ResetMenu;

            controller.ChangeSelectionEvent += ChangeSelection;
            controller.SelectedEvent += Select;
            controller.CanceledEvent += Back;

            totalOptions = gameObject.transform.childCount - 1;
            currentIndex = startIndex;
            cursor = transform.GetChild(totalOptions).gameObject;
            cursor.transform.position = transform.GetChild(currentIndex).position + cursorOffset;
            if (BattleMenuEvent != null)
            {
                BattleMenuEvent();
            }
            if (!controller.enabled)
            {
                controller.enabled = true;
            }
            print("enabled");
        }
    }

    private void OnDisable()
    {
        //turnController.EndTurnEvent -= ResetMenu;

        controller.ChangeSelectionEvent -= ChangeSelection;
        controller.SelectedEvent -= Select;
        controller.CanceledEvent -= Back;

        startIndex = currentIndex;
    }

    private void ChangeSelection(float direction)
    {
        if(direction < 0)
        {
            currentIndex += 1;
            if(currentIndex == totalOptions)
            {
                currentIndex = 0;
            }
        }
        else if(direction > 0)
        {
            currentIndex -= 1;
            if (currentIndex == -1)
            {
                currentIndex = totalOptions-1;
            }
        }
        cursor.transform.position = transform.GetChild(currentIndex).position + cursorOffset;
    }

    private void Select()
    {
        if(this.gameObject.transform.GetChild(currentIndex).GetComponent<MenuButton>().disabled)
        {

        }
        else
        {
            this.gameObject.transform.GetChild(currentIndex).GetComponent<MenuButton>().Click();
        }
        
    }

    public override void Back()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void DisableOption(int option)
    {
        transform.GetChild(option - 1).GetComponent<MenuButton>().disabled = true;
    }

    public void ResetMenu()
    {
        startIndex = 0;
        currentIndex = 0;
        foreach(MenuButton child in this.gameObject.GetComponentsInChildren<MenuButton>())
        {
            child.disabled = false;
        }
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
