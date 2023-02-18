using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public InputMachine machine;
    public State previousState;
    public State[] nextStates;

    public abstract void Activate();

}
