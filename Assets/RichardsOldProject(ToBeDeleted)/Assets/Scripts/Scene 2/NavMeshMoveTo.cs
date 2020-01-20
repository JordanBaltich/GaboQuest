using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveTo : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform destination;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (destination != null)
        {
            agent.SetDestination(destination.position);
        }
    }
}
