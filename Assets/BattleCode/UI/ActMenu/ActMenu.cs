using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActMenu : Menu
{
    public TurnController turnController;
    public int startIndex = 0;
    private int totalOptions;
    private int currentIndex = 0;
    private MenuControls controller;
    private GameObject cursor;
    private Vector3 cursorOffset = new Vector3(-115f, -12.5f, 0);

    private BattleMenu bm;
    private void Awake()
    {
        controller = FindObjectOfType<MenuControls>();
        turnController = FindObjectOfType<TurnController>();
        bm = FindObjectOfType<BattleMenu>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(!disabled)
        {
            controller.ChangeSelectionEvent += ChangeSelection;
            controller.SelectedEvent += Select;
            controller.CanceledEvent += Back;

            totalOptions = gameObject.transform.childCount - 1;
            currentIndex = startIndex;
            cursor = transform.GetChild(totalOptions).gameObject;
            cursor.transform.position = transform.GetChild(currentIndex).position + cursorOffset;
            if (!controller.enabled)
            {
                controller.enabled = true;
            }
        }
        
    }
    private void OnDisable()
    {
        controller.ChangeSelectionEvent -= ChangeSelection;
        controller.SelectedEvent -= Select;
        controller.CanceledEvent -= Back;

        startIndex = currentIndex;
    }

    private void ChangeSelection(float direction)
    {
        if (direction < 0)
        {
            currentIndex += 1;
            if (currentIndex == totalOptions)
            {
                currentIndex = 0;
            }
        }
        else if (direction > 0)
        {
            currentIndex -= 1;
            if (currentIndex == -1)
            {
                currentIndex = totalOptions - 1;
            }
        }
        cursor.transform.position = transform.GetChild(currentIndex).position + cursorOffset;
    }

    private void Select()
    {
        this.gameObject.transform.GetChild(currentIndex).GetComponent<MenuButton>().Click();
    }

    public override void Back()
    {
        bm.disabled = false;
        bm.enabled = true;
        bm.enabled = false;
        bm.enabled = true;
        this.gameObject.SetActive(false);
    }
}
