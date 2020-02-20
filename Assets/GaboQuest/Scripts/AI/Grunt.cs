using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Agent, ITakeDamage
{
    internal void ChargeToLocation(Vector3 position)
    {
        m_navAgent.ResetPath();

        m_navAgent.angularSpeed = 30f;
        m_navAgent.speed = 10f;
        m_navAgent.SetDestination(position);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

}
