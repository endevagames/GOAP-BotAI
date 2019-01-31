using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGetWorldState : GOAPGetWorldState
{
    public override List<GOAPState> getWorldState(GOAPAgent agent) 
    {
        List<GOAPState> worldData = new List<GOAPState>();
        worldData.Add(new GOAPState("Patrol", false));
        return worldData;
    }

    public override List<GOAPState> getGoalState(List<GOAPState> worldState) 
    {
        List<GOAPState> worldData = new List<GOAPState>();
        worldData.Add(new GOAPState("Patrol", true));
        return worldData;
    }
}
