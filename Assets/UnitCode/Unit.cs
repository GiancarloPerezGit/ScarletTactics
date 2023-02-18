using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Class job;
    public Class subJob;
    public Weapon mainHand;
    public HandEquip offHand;
    public Head head;
    public Chest chest;
    public Accessory acc;
    public Tile currentTile;
    public float height;
    public float heightMod;
    public Dictionary<string, StatusEffect> affected = new Dictionary<string, StatusEffect>();
    public List<(string, GameObject)> immune = new List<(string, GameObject)>();
    public List<(string, GameObject)> weak = new List<(string, GameObject)>();
    public List<(string, GameObject)> blocked = new List<(string, GameObject)>();
    public bool alive = true;
}
