using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to return the worldState and goalState of the current agent
public class GOAPGetWorldState : MonoBehaviour
{
    public virtual List<GOAPState> getWorldState(GOAPAgent agent) 
    {
        return null;
    } 
    public virtual List<GOAPState> getGoalState(List<GOAPState> worldState) 
    {
        return null;
    }
}
