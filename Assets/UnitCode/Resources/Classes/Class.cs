using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultRoster", menuName = "Class", order = 3)]
public class Class : ScriptableObject
{
    public int hpM;
    public int mpM;
    public int spM;
    public int paM;
    public int maM;
    public int hpC;
    public int mpC;
    public int spC;
    public int paC;
    public int maC;
    public int mov;
    public int jmp;
    public int cev;

    public List<string> weapons;
    public List<string> armors;
    public List<string> immune;
}
