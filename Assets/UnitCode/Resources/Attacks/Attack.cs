using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public InputMachine machine;
    //Can be reflected with reflect status
    public bool reflect;
    //Can be used with the math command
    public bool calculate;
    //Element affinity of attack
    public Element element;
    //Can be evaded with counterGrasp reaction
    public bool counterGrasp;
    //Can be recasted at user if target has counter magic
    public bool counterMa;
    //Can be recasted at user if target has counter flood
    public bool counterFlood;
    //Can be evaded using evade stats
    public bool ev;
    //5% chance to do more damage and push units
    public bool critical;
    //Mana cost
    public int mp;
    //Time until action is resolved
    public int ctr;
    //How many tiles are affected
    public int aoe;
    //How far above/below the targeted tile the aoe affects
    public int vertTolerance;
    //How far the user can target the ability
    public int horizRange;


    public bool ignoreAllies;
    public bool ignoreCaster;
    public bool ignoreEnemies;


    public Stats unitStats;
    public List<Stats> targetsStats = new List<Stats>();

    public string[] tags;

    public abstract List<Tile> RangeFind();

    public abstract List<Stats> HitTargets();

    public abstract void ApplyEffects();
}
