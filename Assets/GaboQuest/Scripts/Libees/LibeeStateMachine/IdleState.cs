using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleState : StateMachineBehaviour
{
    Rigidbody m_body;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_body = animator.gameObject.GetComponentInParent<Rigidbody>();
        animator.speed *= Random.Range(0.5f, 1f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("RigidbodyVelocity", m_body.velocity.magnitude);
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
