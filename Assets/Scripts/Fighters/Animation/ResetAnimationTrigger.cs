using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationTrigger : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.GetComponent<PlayerController>().SetIsAttackingFalse();
        for(int i = 0; i < animator.parameterCount; i += 1){
            if (animator.GetParameter(i).type == AnimatorControllerParameterType.Trigger)
                animator.ResetTrigger(animator.GetParameter(i).name);
        }
    }
}
