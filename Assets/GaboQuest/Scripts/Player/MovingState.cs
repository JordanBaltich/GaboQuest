using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : StateMachineBehaviour
{
    PlayerController m_Controller;
   
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Controller = animator.GetComponent<PlayerController>();
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_Controller.MovePlayer();

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
