using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Angle
{
    Front,
    Side,
    Back
}
public class TileAttackState : State
{
    public float hitChance;
    public override void Activate()
    {
        if (machine.selectedTile.unit != null)
        {
            hitChance = 0;
            machine.turnController.hitChance = 0;
            GameObject unit = machine.selectedTile.unit;
            Weapon weapon = machine.turnController.activeUnit.GetComponent<Unit>().mainHand;
            weapon.Damage(weapon.XAValue(1, false),0, unit);
            HitChance(CalculateAngle(machine.turnController.activeUnit.GetComponent<Unit>().currentTile.gameObject, unit.GetComponent<Unit>().currentTile.gameObject), machine.selectedTile.unit);
            machine.attackInfo.UpdateUnitInfo(machine.turnController.activeUnit.GetComponent<Stats>());
            machine.attackInfo.UpdateTargetInfo(unit.GetComponent<Stats>());
            machine.attackInfo.UpdateActionInfo(0, -1, (int)hitChance);
            machine.turnController.hitChance = hitChance;
        }
        machine.ChangeState(nextStates[0]);
    }
    public void HitChance(Angle angle, GameObject target)
    {
        //float rolledChance;
        Stats stats = target.GetComponent<Stats>();
        if(angle == Angle.Front)
        {
            hitChance = (100f - stats.pCEv) * (100f - stats.pSEv) * (100f - stats.pAEv) * (100f - stats.wEv) / 1000000f;
        }
        else if(angle == Angle.Side)
        {
            hitChance = (100f - stats.pCEv) * (100f - stats.pAEv) * (100f - stats.wEv) / 10000f;
        }
        else
        {
            hitChance = (100f - stats.pAEv);
        }
        //rolledChance = Random.Range(1f, 100f);
        //if(rolledChance <= hitChance)
        //{
        //    return true;
        //}
        //return false;
    }
    public Angle CalculateAngle(GameObject attacker, GameObject target)
    {
        Vector3 startPos = attacker.transform.position;
        Vector3 endPos = target.transform.position;
        startPos.y = 0;
        endPos.y = 0;
        Vector3 targetDir = startPos - endPos;
        Vector3 forwardVector = target.transform.forward;
        float angle = Vector3.Angle(forwardVector, targetDir);
        if(angle <= 45)
        {
            return Angle.Front;
        }
        else if(angle <= 135)
        {
            return Angle.Side;
        }
        else
        {
            return Angle.Back;
        }
    }

}
