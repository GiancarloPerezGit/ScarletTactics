using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuButton : MonoBehaviour
{
    public bool disabled = false;
    public abstract void click();
}
