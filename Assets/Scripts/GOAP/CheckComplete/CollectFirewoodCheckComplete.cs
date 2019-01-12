using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectFirewoodCheckComplete : GOAPActionCheckComplete 
{
    public override bool checkComplete(GOAPAgent agent)
    {
        var anim = agent.GetComponentInChildren<Animator>();
        if(agent.target != null)
        {
            if(anim.GetBool("hasInteracted"))
            {
                Destroy(agent.target);
                return true;
            }
            else return false;
        }
        else return false;
    }
}