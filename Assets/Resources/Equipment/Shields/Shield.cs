using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shield : HandEquip
{
    public string shieldName;
    public int pEv;
    public int mEv;
    public int price;
    public int eLv;

    public List<StatusEffect> auto;
    public List<StatusEffect> inital;
    public List<StatusEffect> blockStatus;

    public List<Element> absorb;
    public List<Element> weak;
    public List<Element> strengthen;
    public List<Element> half;
    public List<Element> block;

    public int pa;
    public int ma;
    public int sp;
}
