using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    public GameObject menu;

    public bool addToChain;
    public int currentIndex;
    public int totalOptions;

    public GameObject cursor;
    public Vector3 cursorOffset = new Vector3(-115f, -12.5f, 0);

    public List<int> disabledStates = new List<int>();
    public bool startMenuAdded = false;
    public override void Activate()
    {
        if(previousState == null && !startMenuAdded)
        {
            if (!machine.menuChain.Contains(menu))
            {
                machine.menuChain.Add(menu);
                startMenuAdded = true;
            }
        }
        else if(addToChain)
        {
            if (!machine.menuChain.Contains(menu))
            {
                machine.menuChain.Add(menu);
            }
            machine.EnableChain();
        }
        else
        {
            menu.SetActive(true);
        }
        
        totalOptions = 0;
        foreach(Transform child in menu.transform)
        {
            if(child.gameObject.CompareTag("Button"))
            {
                totalOptions += 1;
            }
        }
        if (currentIndex > totalOptions)
        {
            currentIndex = 0;
        }
        cursor.transform.position = menu.transform.GetChild(currentIndex).position + cursorOffset;
        machine.MenuControl();
        machine.menuControls.ChangeSelectionEvent += ChangeSelection;
        machine.menuControls.SelectedEvent += Select;
        machine.menuControls.CanceledEvent += Cancel;

    }

    public void ChangeSelection(float direction)
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
        cursor.transform.position = menu.transform.GetChild(currentIndex).position + cursorOffset;
    }

    public void Select()
    {
        if(!disabledStates.Contains(currentIndex))
        {
            Disconnect();
            if (!addToChain)
            {
                menu.SetActive(false);
            }
            machine.ChangeState(nextStates[currentIndex]);
        }
    }
    
    public void Cancel()
    {
        if(previousState != null)
        {
            Disconnect();
            if(addToChain)
            {
                machine.menuChain.RemoveAt(machine.menuChain.Count - 1);
            }
            menu.SetActive(false);
            machine.ChangeState(previousState);
        }   
    }

    public void Disconnect()
    {
        machine.menuControls.ChangeSelectionEvent -= ChangeSelection;
        machine.menuControls.SelectedEvent -= Select;
        machine.menuControls.CanceledEvent -= Cancel;
    }

    public void DisableState(int state)
    {
        disabledStates.Add(state);
    }
}
