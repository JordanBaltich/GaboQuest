using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : StateMachineBehaviour
{
    PlayerController m_Controller;
    Rigidbody m_Body;
    PlayerMotor m_Motor;
    PlayerTongue m_Tongue;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Controller = animator.GetComponent<PlayerController>();
        m_Body = animator.GetComponent<Rigidbody>();
        m_Motor = animator.GetComponent<PlayerMotor>();
        m_Tongue = animator.GetComponentInChildren<PlayerTongue>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_Controller.MovePlayer();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
