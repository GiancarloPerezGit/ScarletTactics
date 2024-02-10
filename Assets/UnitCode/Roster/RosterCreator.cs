using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RosterCreator : MonoBehaviour
{
    public Controller controller;
    private MapDict md;
    private void Awake()
    {
        md = FindObjectOfType<MapDict>();
        controller = FindObjectOfType<Controller>();
    }

    public void Create(RosterTemplate rt)
    {
        foreach (UnitTemplate ut in rt.units)
        {
            GameObject unit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            unit.GetComponent<MeshRenderer>().material.color = Color.green;
            unit.name = ut.unitName;
            unit.AddComponent<Unit>();
            unit.AddComponent<Stats>();
            unit.GetComponent<Stats>().AddStats(ut.stats);
            unit.GetComponent<Stats>().savedStats = ut.stats;
            unit.GetComponent<Stats>().faction = rt.faction;
            unit.GetComponent<Stats>().job = ut.job1;
            controller.units.Add(unit);
            
            Unit unitInfo = unit.GetComponent<Unit>();

            GameObject afflictList = new GameObject("Afflicted");
            GameObject immuneList = new GameObject("Immune");
            GameObject weakList = new GameObject("Weak");
            GameObject blockList = new GameObject("Blocked");
            afflictList.transform.parent = unit.transform;
            immuneList.transform.parent = unit.transform;
            weakList.transform.parent = unit.transform;
            blockList.transform.parent = unit.transform;

            Class job1 = ut.job1;
            unitInfo.job = job1;
            //Class job2 = ut.job2;
            //unitInfo.subJob = job2;
            if (ut.mainHand != "")
            {

                
                GameObject mainHandObj = Resources.Load<GameObject>("Equipment/Weapons/" + ut.mainHand);
                if (mainHandObj != null)
                {
                    GameObject mainHand = Instantiate(mainHandObj);
                    mainHand.name = ut.mainHand;
                    mainHand.transform.parent = unit.transform;
                    unitInfo.mainHand = mainHand.GetComponent<Weapon>();
                    unitInfo.mainHand.Equip(unit);
                }
            }
            if (ut.offHand != "")
            {
                GameObject offHandWep = (GameObject)Resources.Load("Equipment/Weapons/" + ut.offHand);
                GameObject offHandSh = (GameObject)Resources.Load("Equipment/Shields/" + ut.offHand);
                if (offHandWep != null)
                {
                    GameObject offHand = Instantiate((GameObject)Resources.Load("Equipment/Weapons/" + ut.offHand));
                    offHand.name = ut.offHand;
                    offHand.transform.parent = unit.transform;
                    unitInfo.offHand = offHand.GetComponent<Weapon>();

                }
                else if (offHandSh != null)
                {
                    GameObject offHand = Instantiate((GameObject)offHandSh);
                    offHand.name = ut.offHand;
                    offHand.transform.parent = unit.transform;
                    unitInfo.offHand = offHand.GetComponent<Shield>();
                }
            }
            if (ut.head != "")
            {

            }
            if (ut.chest != "")
            {

            }
            if (ut.acc != "")
            {

            }
            if (ut.tile != null)
            {
                unitInfo.currentTile = md.mapGrid[ut.tile];
                unitInfo.height = unitInfo.currentTile.height;
                unit.gameObject.transform.position = unitInfo.currentTile.transform.position + new Vector3(0,0.5f,0);
            }
            if (ut.stats.movtype == 0)
            {
                unit.AddComponent<Walking>();
            }
            unit.GetComponent<Stats>().UpdateStats();
        }
    }
}
