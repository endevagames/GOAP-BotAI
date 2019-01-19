using UnityEngine;

public class PickupAxeOnActivation : GOAPActionOnActivation 
{
    public override void onActivation(GOAPAgent agent)
    {
        var anim = agent.GetComponentInChildren<Animator>();
        if (anim == null) Debug.Log("Animator doesn't exist");
        else anim.SetTrigger("tr_pickup");
    }  
}