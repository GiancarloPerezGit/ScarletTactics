using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitButton : MenuButton
{
    private TurnController tc;
    private void Awake()
    {
        tc = FindObjectOfType<TurnController>();
    }
    public override void Click()
    {
        tc.EndTurn();
    }

    public override void Back()
    {
        
    }
}
