using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public int duration;
    public GameObject source;
    public bool dispellable;
    public Unit target;

    public abstract void Apply(Unit target);
    
}
