using UnityEngine;
using UnityEngine.AI;

public class FaceNavMeshMovement : MonoBehaviour {

    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponentInParent<NavMeshAgent>();
        agent.updateRotation = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!agent.isStopped)
        {
            var targetPosition = agent.pathEndPosition;
            var targetPoint = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            var _direction = targetPoint - transform.position;
            var _lookRotation = Quaternion.LookRotation(_direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, 10*Time.deltaTime);
        }
	}

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);  
    }
}