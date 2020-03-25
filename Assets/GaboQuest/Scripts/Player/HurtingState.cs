using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtingState : StateMachineBehaviour
{
    Rigidbody m_Body;
    PlayerController m_Controller;

    public Vector3 hitDirection;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Body = animator.GetComponent<Rigidbody>();
        m_Controller = animator.GetComponent<PlayerController>();

        m_Body.AddForce(-m_Body.transform.forward * m_Controller.knockbackForce, ForceMode.Impulse);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_Body.velocity.magnitude <= 1f)
        {
            animator.SetBool("isHit", false);
        }
        else
        {
            m_Body.AddForce(-m_Body.velocity * Mathf.Abs(Physics.gravity.y));
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
