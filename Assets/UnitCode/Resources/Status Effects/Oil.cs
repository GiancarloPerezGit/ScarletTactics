using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : StatusEffect
{
    public override void Apply(Unit target)
    {
        if(target.affected.ContainsKey("Oil"))
        {
            if (target.affected["Oil"].duration < 0)
            {

            }
            else
            {
                StatusEffect old = target.affected["Oil"];
                target.affected["Oil"] = this;
                Destroy(old.gameObject);
            }
        }
        else
        {
            target.affected.Add("Oil", this);
        }
        
        target.gameObject.GetComponent<Stats>().DefendingEvent += Effect;
    }

    public void Effect(string[] tags, Stats source)
    {
        foreach(string tag in tags)
        {
            if(tag == "Fire")
            {
                target.gameObject.GetComponent<Stats>().damageModifier *= 2;
                target.gameObject.GetComponent<Stats>().TargetEvent -= Effect;
                target.affected.Remove("Oil");
                Destroy(this.gameObject);
            }    
        }
    }

    private void Awake()
    {
        duration = -1;
    }


}
