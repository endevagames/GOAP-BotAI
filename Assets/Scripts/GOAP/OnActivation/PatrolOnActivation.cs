using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolOnActivation : GOAPActionOnActivation
{
    public override void onActivation(GOAPAgent agent)
    {
        //Get NavMesh Agent
        NavMeshAgent navAgent = agent.GetComponentInParent<NavMeshAgent>();
        //Set a random destination in the radius
        Vector3 randomDirection = Random.insideUnitSphere * 18;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 18, 1);
        Vector3 finalPosition = hit.position;
        navAgent.SetDestination(finalPosition);
        //Set the animation
        Animator agentAnimator = agent.GetComponentInParent<Animator>();
        agentAnimator.SetBool("walking", true);
    }
}