using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibeeBounceState : StateMachineBehaviour
{
    Rigidbody m_Body;
    BounceArc m_BounceArc;
    LibeeController m_Controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Body = animator.GetComponent<Rigidbody>();
        m_BounceArc = animator.GetComponent<BounceArc>();
        m_Controller = animator.GetComponent<LibeeController>();

        m_Body.isKinematic = true;
        m_BounceArc.BounceOffTarget(m_Controller.bounceTarget);
        m_Controller.bouncing = true;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_BounceArc.MoveAlongArc();

        if (m_BounceArc.currentPosition == m_BounceArc.positions.Length - 1)
        {
            m_Body.isKinematic = false;
            m_Controller.ResetTriggers();
            animator.SetTrigger("isIdle");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        m_BounceArc.currentPosition = 0;
        Destroy(m_BounceArc.currentLandingTarget);
        m_Controller.bouncing = false;
    }
}
