using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
    public override void Activate()
    {
        int ctReduce = 0;
        if(machine.startState.disabledStates.Contains(0))
        {
            ctReduce = 60;
        }
        if(machine.startState.disabledStates.Contains(1))
        {
            if(ctReduce == 60)
            {
                ctReduce = 100;
            }
            else
            {
                ctReduce = 60;
            }
        }
        machine.turnController.activeUnit.GetComponent<Stats>().ct -= ctReduce;
        if(machine.turnController.activeUnit.GetComponent<Stats>().ct > 60)
        {
            machine.turnController.activeUnit.GetComponent<Stats>().ct = 60;
        }
        machine.startState.menu.SetActive(false);
        machine.turnController.EndTurn();
    }
}
