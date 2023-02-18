using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Element
{
    private MapDict md;

    private void Awake()
    {
        md = FindObjectOfType<MapDict>();
    }

    private void Start()
    {
        if(md.weather == 1)
        {
            weatherMultiplier = 0.5f;
        }
        else if(md.weather == 2)
        {
            weatherMultiplier = 2;
        }
    }
}
