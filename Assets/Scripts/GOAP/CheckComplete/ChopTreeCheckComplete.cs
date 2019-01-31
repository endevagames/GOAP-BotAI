using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChopTreeCheckComplete : GOAPActionCheckComplete 
{
    //This script checks if the agent has destroyed the tree it was assigned to chop
    public override bool checkComplete(GOAPAgent agent)
    {
        var anim = agent.GetComponentInChildren<Animator>();
        if(anim.GetBool("attack"))
        {
            if(agent.Target == null) 
            {
                anim.SetBool("attack", false);
                return true;
            }
            else return false;
        }
        return false;
    }
}