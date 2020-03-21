using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibeeShotState : StateMachineBehaviour
{
    BounceArc m_BounceArc;
    LibeeController m_Controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_BounceArc = animator.GetComponent<BounceArc>();
        m_Controller = animator.GetComponent<LibeeController>();

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
