using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "DefaultRoster", menuName = "Roster", order = 2)]
public class RosterTemplate : ScriptableObject
{
    public List<UnitTemplate> units = new List<UnitTemplate>();
    public int faction;
}
