using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : StateMachineBehaviour
{
    PlayerController m_Controller;
    Rigidbody m_Body;
    PlayerMotor m_Motor;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Controller = animator.GetComponent<PlayerController>();
        m_Body = animator.GetComponent<Rigidbody>();
        m_Motor = animator.GetComponent<PlayerMotor>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //convert direction from Vector2 to Vector3
        Vector3 moveDirection = new Vector3(m_Controller.Direction().x, 0, m_Controller.Direction().y);

        //accelerate when input direction is past given threshold, rotate towards input direction
        if (m_Controller.Direction().sqrMagnitude > 0.15f || m_Controller.Direction().sqrMagnitude < -0.15f)
        {

            m_Body.MovePosition(animator.transform.position + moveDirection * m_Motor.Accelerate() * Time.fixedDeltaTime);

            m_Body.rotation = Quaternion.LookRotation((moveDirection * m_Controller.rotationSpeed * Time.deltaTime));
        }
        else
        {
            // decelerate when no input is given
            m_Motor.Decelerate();
            if (m_Body.velocity.sqrMagnitude > 0.15f)
            {
                m_Body.MovePosition(animator.transform.position + moveDirection * m_Motor.Decelerate() * Time.fixedDeltaTime);
            }

        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
