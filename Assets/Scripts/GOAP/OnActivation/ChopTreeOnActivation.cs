using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTreeOnActivation : GOAPActionOnActivation
{
    // Overrides onActivation to set the attack animation trigger
    public override void onActivation(GOAPAgent agent)
    {
        var anim = agent.GetComponentInChildren<Animator>();
        if (anim == null) Debug.Log("Animator doesn't exist");
        else anim.SetTrigger("tr_attack");
    }
}
