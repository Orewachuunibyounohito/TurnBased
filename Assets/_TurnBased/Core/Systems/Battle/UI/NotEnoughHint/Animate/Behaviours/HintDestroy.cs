using UnityEngine;

public class HintDestroy : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        if(stateInfo.normalizedTime > 0.99f){
            Destroy(animator.gameObject);
        }
    }
}
