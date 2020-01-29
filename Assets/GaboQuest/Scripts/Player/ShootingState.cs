using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : StateMachineBehaviour
{
    PlayerController m_Controller;
    Shoot m_Shoot;
    Rigidbody m_Body;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Controller = animator.GetComponent<PlayerController>();
        m_Shoot = animator.GetComponent<Shoot>();
        m_Body = animator.GetComponent<Rigidbody>();

        m_Body.velocity = Vector3.zero;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //convert direction from Vector2 to Vector3
        Vector3 moveDirection = new Vector3(m_Controller.Direction().x, 0, m_Controller.Direction().y);

        m_Body.rotation = Quaternion.LookRotation((moveDirection * m_Controller.rotationSpeed * Time.deltaTime));
        m_Shoot.StartShooting(m_Controller.libee);
      
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
