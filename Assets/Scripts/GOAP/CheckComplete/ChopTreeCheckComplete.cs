using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChopTreeCheckComplete : GOAPActionCheckComplete 
{
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