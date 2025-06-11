using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationStateWatcher : StateMachineBehaviour
{
    public System.Action OnEnter;
    public System.Action OnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnEnter?.Invoke();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnExit?.Invoke();
    }
}
