using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveButton : MenuButton
{
    public BattleMenu bm;
    public MenuControls mc;
    public TurnController tc;
    public TileSelecting ts;
    
    private void Awake()
    {
        bm = FindObjectOfType<BattleMenu>();
        mc = FindObjectOfType<MenuControls>();
        tc = FindObjectOfType<TurnController>();
        ts = FindObjectOfType<TileSelecting>();
    }

    public override void Click()
    {
        bm.enabled = false;
        mc.enabled = false;
        tc.activeUnit.GetComponent<Movement>().StartListening();
        tc.activeUnit.GetComponent<Movement>().RangeFind();
        tc.activeUnit.GetComponent<Movement>().MovementEvent += Done;
        ts.controller.CancelEvent += Back;
        ts.enabled = true;
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void Done()
    {
        tc.activeUnit.GetComponent<Movement>().MovementEvent -= Done;
        tc.activeUnit.GetComponent<Movement>().StopListening();
        ts.controller.CancelEvent -= Back;
        ts.md.ResetTilePaint();
        ts.enabled = false;
        bm.enabled = true;
        bm.DisableOption(1);
        this.gameObject.transform.parent.gameObject.SetActive(true);

    }



    public override void Back()
    {
        tc.activeUnit.GetComponent<Movement>().MovementEvent -= Done;
        tc.activeUnit.GetComponent<Movement>().StopListening();
        ts.controller.CancelEvent -= Back;
        ts.md.ResetTilePaint();
        ts.enabled = false;
        mc.enabled = true;
        bm.enabled = true;
        this.gameObject.transform.parent.gameObject.SetActive(true);
    }
}
