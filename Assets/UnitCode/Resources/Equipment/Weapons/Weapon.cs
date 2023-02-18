using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangeManip
{
    public int lower;
    public int upper;
}

[System.Serializable]
public class StatusManip
{
    public GameObject se;
    public int chance;
}

[System.Serializable]
public class AttackManip
{
    public GameObject attack;
    public int chance;
}

public abstract class Weapon : HandEquip
{
    public string wepName;
    public int wp;
    public int ev;
    public int price;
    public int eLv;
    public Stats unitEquip;

    public RangeManip vertTolerance;
    public RangeManip horizRange;
    
    //Attacking with the weapon has a chance to add or cancel a status effect on the target
    public List<StatusManip> add = new List<StatusManip>();
    public List<StatusManip> cancel = new List<StatusManip>();
    //Equipping the weapon at any time gives the status effect with infinite duration. Status effect cannot be cancelled but
    //will be temporarily disabled if an opposing status effect is applied to the user.
    public List<GameObject> auto = new List<GameObject>();
    //Having the weapon equipped at the start of the battle gives a status effect with regular duration.
    public List<GameObject> inital = new List<GameObject>();
    //Having the weapon equipped prevents the status effect from being applied to the user
    public List<string> blockStatus = new List<string>();

    //Attacking with the weapon has a chance to use the listed attack.
    public AttackManip onHitEffect;

    public int ma;
    public int pa;
    public int sp;

    public Element ele;
    public List<Element> strengthen;

    public abstract bool Equip(GameObject unit);
    public abstract int Damage(int xa, int charge, GameObject target);

    public abstract int XAValue(int modifier, bool crit);
}
