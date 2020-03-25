using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibeeShotState : StateMachineBehaviour
{
    BounceArc m_BounceArc;
    LibeeController m_Controller;
    Rigidbody m_Body;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_BounceArc = animator.GetComponent<BounceArc>();
        m_Controller = animator.GetComponent<LibeeController>();
        m_Body = animator.GetComponent<Rigidbody>();

        m_Body.useGravity = true;
        m_Body.velocity = Vector3.zero;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
