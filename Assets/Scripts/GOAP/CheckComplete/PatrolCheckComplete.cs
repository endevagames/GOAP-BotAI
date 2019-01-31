using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolCheckComplete : GOAPActionCheckComplete
{
    public override bool checkComplete(GOAPAgent agent)
    {
        //Get NavMesh Agent
        NavMeshAgent navAgent = agent.GetComponentInParent<NavMeshAgent>();
        if (!navAgent.pathPending)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f) 
                {
                    //Set the animation
                    Animator agentAnimator = agent.GetComponentInParent<Animator>();
                    agentAnimator.SetBool("walking", false);
                    return true;
                }
            }
        }
        return false;
    }
}
