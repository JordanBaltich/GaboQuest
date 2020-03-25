using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingState : StateMachineBehaviour
{
    Rigidbody m_Body;
    PlayerController m_Controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Body = animator.GetComponent<Rigidbody>();
        m_Controller = animator.GetComponent<PlayerController>();

        m_Body.velocity = Vector3.zero;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
