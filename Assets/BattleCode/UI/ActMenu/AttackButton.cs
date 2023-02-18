using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MenuButton
{
    public BattleMenu bm;
    public MenuControls mc;
    public TurnController tc;
    public TileSelecting ts;
    public GameObject canvas;
    public AttackSequence aSeq;

    private void Awake()
    {
        bm = FindObjectOfType<BattleMenu>();
        mc = FindObjectOfType<MenuControls>();
        tc = FindObjectOfType<TurnController>();
        ts = FindObjectOfType<TileSelecting>();
        aSeq = FindObjectOfType<AttackSequence>();
    }

    public override void Click()
    {
        bm.enabled = false;
        mc.enabled = false;
        ts.controller.CancelEvent += Back;
        ts.enabled = true;
        canvas.SetActive(false);
        aSeq.Begin();
        
    }

    public void Done()
    {
        tc.activeUnit.GetComponent<Unit>().mainHand.GetComponent<WeaponReach>().StopListening();
        ts.controller.CancelEvent -= Back;
        ts.md.ResetTilePaint();
        ts.enabled = false;
        bm.disabled = false;
        bm.enabled = true;
        bm.DisableOption(2);
        mc.enabled = true;
        this.gameObject.transform.parent.gameObject.SetActive(false);
        canvas.SetActive(true);
    }



    public override void Back()
    {
        tc.activeUnit.GetComponent<Unit>().mainHand.GetComponent<WeaponReach>().AttackEvent -= Done;
        tc.activeUnit.GetComponent<Unit>().mainHand.GetComponent<WeaponReach>().StopListening();
        ts.controller.CancelEvent -= Back;
        ts.md.ResetTilePaint();
        ts.enabled = false;
        mc.enabled = true;
        bm.enabled = true;
        this.gameObject.transform.parent.gameObject.SetActive(true);
        canvas.SetActive(true);
    }

}
