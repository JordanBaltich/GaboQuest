using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueState : StateMachineBehaviour
{
    PlayerController m_Controller;
    Rigidbody m_Body;
    PlayerTongue m_Tongue;
    Vector3 LookPoint;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Controller = animator.GetComponent<PlayerController>();
        m_Tongue = animator.GetComponentInChildren<PlayerTongue>();
        m_Body = animator.GetComponent<Rigidbody>();
        m_Tongue.StartCoroutine(m_Tongue.ShootTongue());

        animator.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 Direction = m_Tongue.GrabTarget.transform.position - animator.transform.position;
        LookPoint = new Vector3(Direction.x, 0, Direction.z);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       

        m_Body.rotation = Quaternion.LookRotation(LookPoint.normalized, Vector3.up);

        m_Tongue.RenderTongue();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Tongue.gameObject.SetActive(false);
    }
}
