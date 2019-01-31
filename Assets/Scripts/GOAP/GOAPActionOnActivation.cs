using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The GOAPActionOnActivation class activates the Current Action on the agent
//It should set some variables for the agent that can be used to determine which animation to play and other stuff
public class GOAPActionOnActivation : MonoBehaviour
{
    public virtual void onActivation(GOAPAgent agent) {}
}
