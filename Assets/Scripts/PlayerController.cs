using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //Vars
    public Camera camera;
    public NavMeshAgent agent;
    public Animator anim;
    private GameObject hitObject;
    private int _playerAttackStateHash = Animator.StringToHash("Base Layer.attack1(WeaponOneHand)"); //Replace "PlayerAttack" with the name of your PlayerAttack state in the animator. Don't forget to mention the layer!
    void Update()
    {
        bool shouldMove = agent.velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;
        // Update animation parameters
        anim.SetBool("walk", shouldMove);
        if(shouldMove && (Vector3.Distance(gameObject.transform.position, agent.destination) < 4)) FaceTarget(agent.destination);
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.GetComponent<DamageController>() != null)
        {
            //then if you want to check if you are or not in the PlayerAttack state (meaning that your PlayerAttack animation is being played)
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);        
            if(info.nameHash == _playerAttackStateHash)
            {
                if(info.normalizedTime - Mathf.Floor(info.normalizedTime) > 0.5f) 
                {
                    if(hitObject == null)
                    {
                        col.gameObject.GetComponent<DamageController>().doDamage(1);
                        hitObject = col.gameObject;
                    }
                }
                else hitObject = null;
            }
            else hitObject = null;
        }
        else hitObject = null;
    }
    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);  
    }
}
