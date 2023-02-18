using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Controller controller;
    public SavedStats savedStats;
    
    public int lvl;
    public int exp;
    public int hp;
    public int hpMax;
    public int mp;
    public int mpMax;
    public int sp;
    public int pa;
    public int ma;
    public int ct;
    public int br;
    public int fa;
    public int mov;
    public int jmp;

    public int pCEv;
    public int pSEv;
    public int pAEv;
    public int mSEv;
    public int mAEv;
    public int wEv = 0;


    public int movtype;

    public int facing;

    public int rawHP;
    public int rawMP;
    public int rawSP;
    public int rawPA;
    public int rawMA;


    public int zodiac;
    public int faction;

    public Class job;

    public int aimModifier = 1;
    public int damageModifier = 1;
    

    private void Awake()
    {
        controller = FindObjectOfType<Controller>();
    }

    public delegate void TargetDelegate(string[] tags, Stats source);
    public event TargetDelegate TargetEvent;

    public void Targeted(string[] tags, Stats source)
    {
        TargetEvent?.Invoke(tags, source);
    }

    public delegate void AimDelegate(string[] tags);
    public event AimDelegate AimEvent;

    public void Aimed(string[] tags)
    {
        aimModifier = 1;
        AimEvent?.Invoke(tags);
    }

    public delegate void AttackingDelegate(string[] tags);
    public event AttackingDelegate AttackingEvent;

    public void Attacking(string[] tags)
    {
        damageModifier = 1;
        AttackingEvent?.Invoke(tags);
    }

    public delegate void DefendingDelegate(string[] tags, Stats source);
    public event DefendingDelegate DefendingEvent;

    public void Defending(string[] tags, Stats source)
    {
        DefendingEvent?.Invoke(tags, source);
    }

    public void AddStats(SavedStats ss)
    {
        lvl = ss.lvl;
        exp = ss.exp;
        ct = ss.ct;
        br = ss.br;
        fa = ss.fa;
        mov = ss.mov;
        jmp = ss.jmp;
        pCEv = ss.cEv;
        movtype = ss.movtype;
        rawHP = ss.rawHP;
        rawMP = ss.rawMP;
        rawSP = ss.rawSP;
        rawPA = ss.rawPA;
        rawMA = ss.rawMA;
        zodiac = ss.zodiac;
        faction = ss.faction;
    }

    public bool AddCt()
    {
        ct += sp;
        if(ct >= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateStats()
    {
        hpMax = (rawHP * job.hpM) / 1638400;
        mpMax = (rawMP * job.mpM) / 1638400;
        sp = (rawSP * job.spM) / 1638400;
        pa = (rawPA * job.paM) / 1638400;
        ma = (rawMA * job.maM) / 1638400;
        hp = hpMax;
        mp = mpMax;
    }

    public void LevelUp()
    {
        rawHP += rawHP / (job.hpC + lvl);
        rawMP += rawMP / (job.mpC + lvl);
        rawSP += rawSP / (job.spC + lvl);
        rawPA += rawPA / (job.paC + lvl);
        rawMA += rawMA / (job.maC + lvl);

        UpdateStats();

        exp -= 100;
        lvl += 1;
    }

}
