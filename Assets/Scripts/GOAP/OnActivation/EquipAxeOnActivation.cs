using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAxeOnActivation : GOAPActionOnActivation
{
    // Start is called before the first frame update
    public override void onActivation(GOAPAgent agent)
    {
        var anim = agent.GetComponentInChildren<Animator>();
        if (anim == null) Debug.Log("Animator does not exist");
        else
        {
            if (!anim.GetBool("swordEquipped")) anim.SetTrigger("equipSword");
        }
    }
}
