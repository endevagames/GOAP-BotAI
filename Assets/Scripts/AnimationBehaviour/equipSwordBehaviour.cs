using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipSwordBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("swordEquipped", true);
        var player = animator.GetComponentInParent<GOAPAgent>();
        if (player != null)
        {
            for(var i = 0; i < player.backpackList.Count; i++)
            {
                var item = player.backpackList[i];
                if(item.name == "Sword") --item.count;
            }
            int itemIndex = -1;
            for(var i = 0; i < player.carryingList.Count; i++)
            {
                var item = player.carryingList[i];
                if(item.name == "Sword") itemIndex = i;
            }
            if(itemIndex >= 0) ++player.carryingList[itemIndex].count;
            else player.carryingList.Add(new GOAPAgent.InventoryItem("Sword", 1));
        }
        else Debug.Log("Agent animator null");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
