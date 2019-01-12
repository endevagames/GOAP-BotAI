using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPAction : MonoBehaviour
{
    public string actionName;

    public List<GOAPState> preconditions;
    public List<GOAPState> effects;
    public float cost;
    public string targetTag;
    public bool requiresInRange;
    public float range;
    private bool inRange;
    public GOAPActionOnActivation onActivation;
    public GOAPActionCheckComplete checkComplete;

    public bool CanResolve(List<GOAPState> states)
    {
        for(int i = 0; i < effects.Count; i++)
        {
            var effect = effects[i];
            for(var j = 0; j < states.Count; j++)
            {
                var state = states[j];
                if (state.name != effect.name) continue;   
                if (state.val == effect.val) return true;
            }
        }
        return false;
    }
    public List<GOAPState> UnsetStateEffects(List<GOAPState> states)
    {
        var _myLocalList = new List<GOAPState>();
        for(var i = 0; i < states.Count; i++) _myLocalList.Add(new GOAPState(states[i].name, states[i].val));
        for (int i = 0; i < effects.Count; i++)
        {
            var effect = effects[i];
            for (var j = 0; j < _myLocalList.Count; j++)
            {
                var state = _myLocalList[j];
                if (state.name != effect.name) continue;
                if(state.val == effect.val) _myLocalList.Remove(state);
            }
        }
        return _myLocalList;
    }

    public List<GOAPState> SetStatePrecons(List<GOAPState> states)
    {
        var _myLocalList = new List<GOAPState>();
        for(var i = 0; i < states.Count; i++) _myLocalList.Add(new GOAPState(states[i].name, states[i].val));
        for (int i = 0; i < preconditions.Count; i++)
        {
            var precon = preconditions[i];
            _myLocalList.Add(precon);
        }
        return _myLocalList;
    }

    public void activate(GOAPAgent agent)
    {
        if(onActivation != null) onActivation.onActivation(agent);
        else Debug.Log("ERROR! Action: " + actionName + " does not contain an onActivate behaviour");
    }

    public bool isDone(GOAPAgent agent)
    {
        if(checkComplete != null) 
        {
            return checkComplete.checkComplete(agent);
        }
        else 
        {
            Debug.Log("ERROR! Action: " + actionName + " has no checkComplete behaviour.");
            return true;
        }
    }
}
