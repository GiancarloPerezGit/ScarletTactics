using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TargetMenuState : MenuState
{
    public GameObject damageInfo;
    public GameObject unitInfo;


    //private bool startMenuAdded = false;
    public override void Activate()
    {
        if (previousState == null && !startMenuAdded)
        {
            if(!machine.menuChain.Contains(menu))
            {
                machine.menuChain.Add(menu);
                startMenuAdded = true;
            }
            
        }
        else if (addToChain)
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
        foreach (Transform child in menu.transform)
        {
            if (child.gameObject.CompareTag("Button"))
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
}
