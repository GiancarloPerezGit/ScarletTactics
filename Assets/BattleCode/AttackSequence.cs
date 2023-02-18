using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSequence : MonoBehaviour
{
    public MenuButton source;

    public MenuControls mc;
    public TileControls tc;
    public TileSelecting ts;

    public GameObject unit;

    public bool rangeFind;
    public bool spreadFind;
    public bool damagePredict;

    public void Begin()
    {
        unit.GetComponent<Unit>().mainHand.GetComponent<WeaponReach>().StartListening();
        unit.GetComponent<Unit>().mainHand.GetComponent<WeaponReach>().RangeFind();
        unit.GetComponent<Unit>().mainHand.GetComponent<WeaponReach>().AttackEvent += Cleave;
    }

    public void Cleave()
    {

    }
}
