using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueState : StateMachineBehaviour
{
    PlayerController m_Controller;
    PlayerTongue m_Tongue;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Controller = animator.GetComponent<PlayerController>();
        m_Tongue = animator.GetComponentInChildren<PlayerTongue>();

        m_Tongue.StartCoroutine(m_Tongue.ShootTongue());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        m_Tongue.RenderTongue();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Tongue.gameObject.SetActive(false);
    }
}
