using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : Agent
{
    Health m_Health;
    [SerializeField] int libeeLayer;

    private void Awake()
    {
        m_Health = GetComponentInParent<Health>();
    }

    private void Start()
    {
        Player = GameObject.Find("Player");
        m_navAgent = GetComponent<NavMeshAgent>();
    }

    internal void ChargeToLocation(Vector3 position)
    {
        m_navAgent.ResetPath();
        m_navAgent.SetDestination(position);
    }

}
