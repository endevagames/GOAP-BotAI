using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectFirewoodCheckComplete : GOAPActionCheckComplete 
{
    public override bool checkComplete(GOAPAgent agent)
    {
        var anim = agent.GetComponentInChildren<Animator>();
        int _playerAttackStateHash = Animator.StringToHash("Base Layer.interact");
        if(agent.target != null)
        {
            if(anim.GetBool("hasInteracted"))
            {
                Destroy(agent.target.gameObject);
                anim.SetBool("hasInteracted", false);
                return true;
            }
            else return false;
        }
        else return false;
    }
}