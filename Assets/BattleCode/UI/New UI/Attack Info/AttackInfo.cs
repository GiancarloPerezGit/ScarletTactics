using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackInfo : MonoBehaviour
{
    public GameObject menu;
    public GameObject targetInfo;
    public GameObject unitInfo;
    
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

    public void TurnOff()
    {
        unitInfo.SetActive(false);
        menu.SetActive(false);
    }
}
