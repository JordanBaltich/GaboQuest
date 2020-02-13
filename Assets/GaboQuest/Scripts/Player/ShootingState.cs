using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : StateMachineBehaviour
{
    PlayerController m_Controller;
    Shoot m_Shoot;
    Rigidbody m_Body;
    GrowShrinkMechanic m_GrowMechanic;

    Vector3 moveDirection;
    Vector3 lastHeldDirection;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Controller = animator.GetComponent<PlayerController>();
        m_Shoot = animator.GetComponent<Shoot>();
        m_Body = animator.GetComponent<Rigidbody>();
        m_GrowMechanic = animator.GetComponent<GrowShrinkMechanic>();

        m_Body.velocity = Vector3.zero;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //convert direction from Vector2 to Vector3
        moveDirection = new Vector3(m_Controller.Direction().x, 0, m_Controller.Direction().y);

        //accelerate when input direction is past given threshold, rotate towards input direction
        if (m_Controller.Direction().sqrMagnitude > 0.15f || m_Controller.Direction().sqrMagnitude < -0.15f)
        {
            m_Body.rotation = Quaternion.LookRotation((moveDirection * m_Controller.rotationSpeed * Time.deltaTime));

        }

        if (m_Controller.player.GetButton("Shoot"))
        {
            m_Shoot.StartShooting(m_Controller.m_LibeeSorter.Normal);

        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Shoot.StopShooting();
    }


}
