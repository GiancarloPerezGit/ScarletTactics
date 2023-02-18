using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Menu : MonoBehaviour
{
    public int index;
    public bool disabled = false;
    public abstract void Back();
    
}
