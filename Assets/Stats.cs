using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Controller controller;
    public Class job;

    public int lvl;
    public int exp;
    public int hp;
    public int mp;
    public int ct;
    public int br;
    public int fa;
    public int mov;
    public int jmp;
    public int spd;
    public int movtype;

    private int rawHP = 501258;
    private int rawMP = 230674;
    private int rawSP;
    private int rawPA;
    private int rawMA;


    public int zodiac;
    public int faction;
    public bool alive = true;

    private void Start()
    {
        
    }

    public bool addCt()
    {
        ct += spd;
        if(ct >= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
