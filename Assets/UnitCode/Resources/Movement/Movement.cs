using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : RangeFinder
{
    public abstract void StartListening();
    public abstract void StopListening();

    public delegate void MovementDelegate();

    public abstract event MovementDelegate MovementEvent;

}
