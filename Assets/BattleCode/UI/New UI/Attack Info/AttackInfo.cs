using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum damageModifier
{
    HP = 0,
    MP = 1,
    CT = 2
}
public class AttackInfo : MonoBehaviour
{
    public GameObject menu;
    public GameObject targetInfo;
    public GameObject unitInfo;
    public GameObject damage;

    public TextMeshProUGUI targetHpCurr;
    public TextMeshProUGUI targetHpMax;
    public TextMeshProUGUI targetMpCurr;
    public TextMeshProUGUI targetMpMax;
    public TextMeshProUGUI targetCtCurr;
    public TextMeshProUGUI targetCtMax;

    public TextMeshProUGUI unitHpCurr;
    public TextMeshProUGUI unitHpMax;
    public TextMeshProUGUI unitMpCurr;
    public TextMeshProUGUI unitMpMax;
    public TextMeshProUGUI unitCtCurr;
    public TextMeshProUGUI unitCtMax;

    public TextMeshProUGUI damageSign;
    public TextMeshProUGUI damageAmount;
    public TextMeshProUGUI damageType;
    public TextMeshProUGUI damageChance;



    public void UpdateTargetInfo(Stats stats)
    {
        targetHpCurr.text = stats.hp.ToString();
        targetHpMax.text = stats.hpMax.ToString();
        targetMpCurr.text = stats.mp.ToString();
        targetMpMax.text = stats.mpMax.ToString();
        targetCtCurr.text = stats.ct.ToString();
        targetCtMax.text = "100";
        targetInfo.SetActive(true);
        menu.SetActive(true);
    }

    public void UpdateUnitInfo(Stats stats)
    {
        unitHpCurr.text = stats.hp.ToString();
        unitHpMax.text = stats.hpMax.ToString();
        unitMpCurr.text = stats.mp.ToString();
        unitMpMax.text = stats.mpMax.ToString();
        unitCtCurr.text = stats.ct.ToString();
        unitCtMax.text = "100";
        unitInfo.SetActive(true);
        menu.SetActive(true);
    }

    public void UpdateActionInfo(damageModifier dm, int damageAmount, int successChance)
    {
        switch (dm)
        {
            case damageModifier.HP:
                damageType.text = "HP";
                break;
            case damageModifier.MP:
                damageType.text = "MP";
                break;
            case damageModifier.CT:
                damageType.text = "CT";
                break;
        }
        if(damageAmount < 0)
        {
            damageSign.text = "-";
        }
        else if(damageAmount > 0)
        {
            damageSign.text = "+";
        }
        damageChance.text = successChance.ToString();
        damage.SetActive(true);
        menu.SetActive(true);
    }

    public void TurnOff()
    {
        unitInfo.SetActive(false);
        menu.SetActive(false);
    }
}
