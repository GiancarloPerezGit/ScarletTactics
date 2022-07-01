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

    public override void click()
    {
        bm.enabled = false;
        mc.enabled = false;
        tc.activeUnit.GetComponent<Walking>().StartListening();
        tc.activeUnit.GetComponent<Walking>().pathfind();
        tc.activeUnit.GetComponent<Walking>().MovementEvent += Done;
        ts.controller.CancelEvent += Back;
        ts.enabled = true;
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void Done()
    {
        tc.activeUnit.GetComponent<Walking>().MovementEvent -= Done;
        tc.activeUnit.GetComponent<Walking>().StopListening();
        ts.controller.CancelEvent -= Back;
        ts.md.resetTiles();
        ts.enabled = false;
        bm.enabled = true;
        bm.DisableOption(1);
        this.gameObject.transform.parent.gameObject.SetActive(true);

    }



    public void Back()
    {
        tc.activeUnit.GetComponent<Walking>().MovementEvent -= Done;
        tc.activeUnit.GetComponent<Walking>().StopListening();
        ts.controller.CancelEvent -= Back;
        ts.md.resetTiles();
        ts.enabled = false;
        mc.enabled = true;
        bm.enabled = true;
        this.gameObject.transform.parent.gameObject.SetActive(true);
    }
}
