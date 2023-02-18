using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultRoster", menuName = "Stats", order = 4)]
public class SavedStats : ScriptableObject
{
    public int lvl;
    public int exp;
    public int hp;
    public int hpMax;
    public int mp;
    public int mpMax;
    public int spd;
    public int pa;
    public int ma;
    public int ct;
    public int br;
    public int fa;
    public int mov;
    public int jmp;
    public int cEv;

    public int movtype;

    public int rawHP;
    public int rawMP;
    public int rawSP;
    public int rawPA;
    public int rawMA;


    public int zodiac;
    public int faction;
}
