using UnityEngine;

public class CollectWoodOnActivation : GOAPActionOnActivation 
{
    public override void onActivation(GOAPAgent agent)
    {
        //Overrides onActivation to set the pickup animation trigger
        var anim = agent.GetComponentInChildren<Animator>();
        if (anim == null) Debug.Log("Animator doesn't exist");
        else anim.SetTrigger("tr_pickup");
    }  
}