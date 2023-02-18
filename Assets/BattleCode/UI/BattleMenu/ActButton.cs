using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActButton : MenuButton
{
    public GameObject actPanel;
    public BattleMenu bm;

    private void Awake()
    {
        bm = FindObjectOfType<BattleMenu>();
    }

    public override void Click()
    {
        bm.enabled = false;
        bm.disabled = true;
        actPanel.SetActive(true);
    }

    public override void Back()
    {

    }
}
