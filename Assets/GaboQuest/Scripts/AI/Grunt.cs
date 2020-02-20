using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Agent
{
    Health m_Health;
    [SerializeField] int libeeLayer;

    private void Awake()
    {
        m_Health = GetComponentInParent<Health>();
    }

    internal void ChargeToLocation(Vector3 position)
    {
        m_navAgent.ResetPath();

        m_navAgent.angularSpeed = 30f;
        m_navAgent.speed = 10f;
        m_navAgent.SetDestination(position);
    }

}
