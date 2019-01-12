using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Priority_Queue;

public class GOAPPlanner
{
    public Queue<GOAPAction> GetPlan(List<GOAPState> startState, List<GOAPState> goalState, List<GOAPAction> actionList)
    {
        Queue<GOAPAction> actionQueue = new Queue<GOAPAction>();
        //Create a state comparer to compare the states
        StateComparer stateComparer = new StateComparer();
        //Opened List contains list of nodes that are still to be assessed
        //Closed List contains list of nodes that have been processed
        //previousNodes stores the links between nodes
        SimplePriorityQueue<List<GOAPState>> openedList = new SimplePriorityQueue<List<GOAPState>>();
        Dictionary<List<GOAPState>, bool> closedList = new Dictionary<List<GOAPState>, bool>( stateComparer);
        Dictionary<List<GOAPState>, List<GOAPState>> previousNodesState = new Dictionary<List<GOAPState>, List<GOAPState>>(stateComparer);
        Dictionary<List<GOAPState>, GOAPAction> previousNodesAction = new Dictionary<List<GOAPState>, GOAPAction>(stateComparer);
        Dictionary<List<GOAPState>, float> costs = new Dictionary<List<GOAPState>, float>( stateComparer);

        var node = goalState;
        node.OrderBy(n => n.name).ToList();
        openedList.Enqueue(node, 1);
        costs.Add(node, 0);
        while (openedList.Count > 0)
        {
            node = openedList.First;
            //Check if we've found the solution
            if (GoalNodeReached(startState, node)) break;
            openedList.Dequeue();
            //Add to the closedList
            closedList.Add(node, true);
            //Get list of neighbours
            for(var i = 0; i < actionList.Count; i++)
            {
                var action = actionList[i];
                if(action.CanResolve(node))
                {
                    //Create a new state based on the effects and preconditions of the current action
                    List<GOAPState> tempState = action.UnsetStateEffects(node);
                    tempState = action.SetStatePrecons(tempState);
                    tempState.OrderBy(n => n.name).ToList();
                    if (closedList.ContainsKey(tempState)) continue;
                    float F, G, H;
                    if(!costs.ContainsKey(tempState))
                    {
                        G = costs[node] + StateGetDist(goalState, tempState);
                        costs.Add(tempState, G);
                    }
                    else G = costs[tempState];
                    H = action.cost;
                    //Calculate the current cost of the path to the current action plus it's own cost
                    F = G + H;
                    var lastF = -1f;
                    if (openedList.TryGetPriority(tempState, out lastF))
                    {
                        if (F < lastF) 
                        {
                            openedList.UpdatePriority(tempState, F);
                            //Keep track of the states and the edges (actions) that lead up to each state
                            if(!previousNodesState.ContainsKey(tempState)) previousNodesState.Add(tempState, node);
                            else previousNodesState[tempState] = node;
                            if(!previousNodesAction.ContainsKey(tempState)) previousNodesAction.Add(tempState, action);
                            else previousNodesAction[tempState] = action;
                        }
                    }
                    else
                    {
                        openedList.Enqueue(tempState, F);
                        //Keep track of the states and the edges (actions) that lead up to each state
                        if(!previousNodesState.ContainsKey(tempState)) previousNodesState.Add(tempState, node);
                        else previousNodesState[tempState] = node;
                        if(!previousNodesAction.ContainsKey(tempState)) previousNodesAction.Add(tempState, action);
                        else previousNodesAction[tempState] = action;
                    }
                }

            }
        }

        //If we've found the startState, our regressive search has finished
        if (GoalNodeReached(startState, node))
        {
            //Ensure the actionQueue is clear
            actionQueue.Clear();
            while (previousNodesAction.ContainsKey(node))
            {
                //Build the new plan
                var action = previousNodesAction[node];
                actionQueue.Enqueue(action);
                previousNodesAction.Remove(node);
                node = previousNodesState[node];
            }
        }
        //Return the plan
        return actionQueue;
    }

    public int StateGetDist(List<GOAPState> x, List<GOAPState> y)
    {
        int dist = Mathf.Abs(x.Count - y.Count);
        for (int i = 0; i < x.Count; i++)
        {
            for(int j = 0; j < y.Count; j++)
            {
                if(x[i].name != y[i].name) continue;
                    else if(x[i].val != y[i].val) ++dist;
            }
        }
        return dist;
    }

    public bool GoalNodeReached(List<GOAPState> startState, List<GOAPState> sampleState)
    {
        for(var j = 0; j < sampleState.Count; j++)
        {
            for(var i = 0; i < startState.Count; i++)
            {
                if(sampleState[j].name != startState[i].name) continue;
                    else if(sampleState[j].val != startState[i].val) return false;
            }
        }
        return true;
    }

}

//State comparer is used to compare states in the list of states
public class StateComparer : IEqualityComparer<List<GOAPState>>
{
    public bool Equals(List<GOAPState> x, List<GOAPState> y)
    {
        if (x.Count != y.Count)
        {
            return false;
        }
        for (int i = 0; i < x.Count; i++)
        {
            if (x[i].name != y[i].name)
            {
                return false;
            }
            if (x[i].val != y[i].val)
            {
                return false;
            }
        }
        return true;
    }

    public int GetHashCode(List<GOAPState> obj)
    {
        int result = 0;
        for (int i = 0; i < obj.Count; i++)
        {
            if (obj[i].val == true) result += i ^ 2;
        }
        return result;
    }
}
