using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectFirewoodCheckComplete : GOAPActionCheckComplete 
{
    //This script checks to see if the firewood is still available
    public override bool checkComplete(GOAPAgent agent)
    {
        var anim = agent.GetComponentInChildren<Animator>();
        int _playerAttackStateHash = Animator.StringToHash("Base Layer.interact");
        if(agent.Target != null)
        {
            if(anim.GetBool("hasPickedUp"))
            {
                Destroy(agent.Target.gameObject);
                anim.SetBool("hasPickedUp", false);
                return true;
            }
            else return false;
        }
        else return false;
    }
}