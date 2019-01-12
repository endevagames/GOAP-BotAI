using UnityEngine;

public class CollectWoodOnActivation : GOAPActionOnActivation 
{
    public override void onActivation(GOAPAgent agent)
    {
        var anim = agent.GetComponentInChildren<Animator>();
        if (anim == null) Debug.Log("Animator doesn't exist");
        else anim.SetTrigger("interact");
    }  
}