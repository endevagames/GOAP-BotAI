using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetWorldState : GOAPGetWorldState 
{
    public override List<GOAPState> getWorldState(GOAPAgent agent) 
    {
		List<GOAPState> worldData = new List<GOAPState>();
        var controller = agent.GetComponentInParent<PlayerController>();
        bool hasAxe = false;
        if((controller != null) && (controller.hand.GetComponentInChildren<InteractableItemBase>() != null)) hasAxe = (controller.hand.GetComponentInChildren<InteractableItemBase>().tag != null);
        worldData.Add(new GOAPState("FirewoodCollected", false));
        worldData.Add(new GOAPState("HasAxe", hasAxe));
        worldData.Add(new GOAPState("FirewoodAvailable", GameObject.FindGameObjectsWithTag("Firewood").Length > 0));
        return worldData;
    }

    public override List<GOAPState> getGoalState(List<GOAPState> worldState) 
    {
        List<GOAPState> worldData = new List<GOAPState>();
        worldData.Add(new GOAPState("FirewoodCollected", true));
        return worldData;
    }
}