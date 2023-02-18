using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreFightSetup : MonoBehaviour
{
    public RosterTemplate playerRoster;
    public List<RosterTemplate> enemies;
    public List<RosterTemplate> allies;

    public RosterCreator rc;
    public Controller controller;

    private void Awake()
    {
        rc = FindObjectOfType<RosterCreator>();
        controller = FindObjectOfType<Controller>();
    }

    private void Start()
    {
        foreach(RosterTemplate rt in enemies)
        {
            rc.Create(rt);
        }
        if(allies != null)
        {
            foreach (RosterTemplate rt in allies)
            {
                rc.Create(rt);
            }
        }
        rc.Create(playerRoster);
        controller.Begin();
    }
}
