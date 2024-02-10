using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{
    public override bool Equip(GameObject unit)
    {
        if(unit.GetComponent<Unit>().job.weapons.Contains("Dagger"))
        {
            unitEquip = unit.GetComponent<Stats>();
            unit.GetComponent<Stats>().pa += pa;
            unit.GetComponent<Stats>().ma += ma;
            unit.GetComponent<Stats>().sp += sp;
            foreach (string se in blockStatus)
            {
                unit.GetComponent<Unit>().immune.Add((se, this.gameObject));
            }
            if (!unit.GetComponent<Stats>().controller.enabled)
            {
                foreach (GameObject se in inital)
                {
                    GameObject status = Instantiate(se, this.gameObject.transform);
                    status.GetComponent<StatusEffect>().target = unit.GetComponent<Unit>();
                    status.GetComponent<StatusEffect>().Apply(unit.GetComponent<Unit>());
                    
                }
            }
            foreach (GameObject se in auto)
            {
                GameObject status = Instantiate(se, this.gameObject.transform);
                status.name = se.name;
                status.GetComponent<StatusEffect>().duration = -1;
                status.GetComponent<StatusEffect>().source = gameObject;
                status.GetComponent<StatusEffect>().target = unit.GetComponent<Unit>();
                status.GetComponent<StatusEffect>().Apply(unit.GetComponent<Unit>());
            }
        }
        return false;
    }

    public override int Damage(int xa, int charge, GameObject target)
    {
        if(add.Count > 0)
        {
            foreach(StatusManip sM in add)
            {
                if (Random.Range(1, 101) < sM.chance)
                {
                    GameObject status = Instantiate(sM.se, target.transform);
                    status.GetComponent<StatusEffect>().target = target.GetComponent<Unit>();
                    status.GetComponent<StatusEffect>().Apply(target.GetComponent<Unit>());
                }
            }
        }
        if(cancel.Count > 0)
        {
            foreach (StatusManip sM in cancel)
            {
                if (Random.Range(1, 101) < sM.chance)
                {
                    if (target.GetComponent<Unit>().affected.ContainsKey(sM.se.name))
                    {
                        StatusEffect statusEffect = target.GetComponent<Unit>().affected[sM.se.name];
                        target.GetComponent<Unit>().affected.Remove(sM.se.name);
                        Destroy(statusEffect.gameObject);
                    }
                }
            }
        }
        return Mathf.FloorToInt(xa * wp);
    }

    public override int XAValue(int modifier, bool crit)
    {
        int baseXa = (unitEquip.pa + unitEquip.sp) / 2;
        if(crit)
        {
            if(Random.Range(1,101) <= 5)
            {
                baseXa = baseXa + Random.Range(1, baseXa + 1) - 1;
            }
        }
        return baseXa * modifier;
    }

    public override int HitChance()
    {
        throw new System.NotImplementedException();
    }
}
