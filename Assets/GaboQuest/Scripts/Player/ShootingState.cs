﻿using System.Collections;
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

        Vector3 Direction = m_Controller.AimPoint.position - animator.transform.position;
        Vector3 LookPoint = new Vector3(Direction.x, 0, Direction.z);

        m_Body.rotation = Quaternion.LookRotation(LookPoint.normalized, Vector3.up);

        if (m_Controller.player.GetButton("Shoot"))
        {
            m_Shoot.StartCharge(m_Controller.player);

        }

        if (m_Controller.player.GetButtonUp("Shoot"))
        {
            m_Shoot.FireBullet(m_Controller.m_LibeeSorter.CurrentAmmoPool());
            m_Shoot.StopCharge();

            m_Controller.m_LibeeSorter.SortLibee();

        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
