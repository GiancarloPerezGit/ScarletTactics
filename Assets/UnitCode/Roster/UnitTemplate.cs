using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "DefaultUnit", menuName = "Unit", order = 1)]
public class UnitTemplate : ScriptableObject
{
    public string sprite;
    public string unitName;
    public Class job1;
    public Class job2;
    public string mainHand;
    public string offHand;
    public string head;
    public string chest;
    public string acc;
    public SavedStats stats;
    public Vector3 tile;
}
