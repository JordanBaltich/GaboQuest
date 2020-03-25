using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibeeInventoryState : StateMachineBehaviour
{
    Rigidbody m_Body;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Body = animator.GetComponent<Rigidbody>();

        animator.transform.parent = null;
        m_Body.useGravity = false;
        m_Body.velocity = Vector3.zero;


    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
