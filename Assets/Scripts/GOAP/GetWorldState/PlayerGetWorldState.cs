using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetWorldState : GOAPGetWorldState 
{
    public override List<GOAPState> getWorldState(GOAPAgent agent) 
    {
		List<GOAPState> worldData = new List<GOAPState>();
        bool hasSword = false;
        for(var i = 0; i < agent.carryingList.Count; i++)
        {
            var item = agent.carryingList[i];
            if((item.name == "Sword") && (item.count > 0)) hasSword = true;
        }
        worldData.Add(new GOAPState("FirewoodCollected", false));
        worldData.Add(new GOAPState("SwordEquipped", hasSword));
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