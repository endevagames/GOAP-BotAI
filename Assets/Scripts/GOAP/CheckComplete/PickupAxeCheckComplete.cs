using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupAxeCheckComplete : GOAPActionCheckComplete 
{
    public override bool checkComplete(GOAPAgent agent)
    {
        var anim = agent.GetComponentInChildren<Animator>();
        if(agent.target != null)
        {
            if(anim.GetBool("hasPickedUp"))
            {
                var controller = agent.GetComponentInParent<PlayerController>();
                if(controller != null) controller.SetHandObject(agent.target);
                anim.SetBool("hasPickedUp", false);
                return true;
            }
            else return false;
        }
        else return false;
    }
}