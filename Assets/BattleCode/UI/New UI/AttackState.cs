using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override void Activate()
    {
        GameObject weapon = machine.turnController.activeUnit.GetComponent<Unit>().mainHand.gameObject;
        weapon.GetComponent<WepAttack>().HitTargets();
        if(weapon.GetComponent<WepAttack>().targetsStats.Count != 0)
        {

        }
    }
}
