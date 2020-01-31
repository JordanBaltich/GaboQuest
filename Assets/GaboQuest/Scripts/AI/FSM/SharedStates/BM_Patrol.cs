using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using System;

public class BM_Patrol : StateBehaviour
{
    Agent m_agent;

    float sqrDistanceToDestination;
    public float distanceToArrive;
    float sqrDistanceToArrive;

    Vector3Var destination;

    GameObjectVar target;

    GameObjectVar targetWaypoint;

    // Called when the state is enabled
    void OnEnable () {
		Debug.Log("Started Patrol");

        Setup();
        m_agent.GoToWaypoint();

        StartCoroutine(CheckForEnemy());
        StartCoroutine(CheckDistanceFromWaypoint());
    }

    

    // Called when the state is disabled
    void OnDisable () {

		Debug.Log("Stopped Patrol");

        StopAllCoroutines();
	}

    private void Setup()
    {
        m_agent = GetComponent<Agent>();
        target = blackboard.GetGameObjectVar("Threat");
        targetWaypoint.Value = m_agent.waypointTransform[m_agent.currentWaypointIndex];

        sqrDistanceToArrive = distanceToArrive * distanceToArrive;

        m_agent.ResetAgent();

        updateDistanceToDestination();

    }

    private void updateDistanceToDestination()
    {
        sqrDistanceToDestination = (m_agent.waypointTransform[m_agent.currentWaypointIndex].transform.position - transform.position).sqrMagnitude;
    }

    IEnumerator CheckForEnemy()
    {
        while (enabled)
        {
            if (target.Value != null)
            {
                SendEvent("FoundEnemy");
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator CheckDistanceFromWaypoint()
    {
        while (enabled)
        {
            //increment waypoint if distance is reached
            if (sqrDistanceToDestination < sqrDistanceToArrive)
            {
                //cyclic patrol
                m_agent.currentWaypointIndex = (m_agent.currentWaypointIndex + 1) % m_agent.waypointTransform.Length;
                destination.Value = m_agent.waypointTransform[m_agent.currentWaypointIndex].transform.position;

                SendEvent("Idling");

                yield return null;
            }
            updateDistanceToDestination();

            yield return new WaitForSeconds(.2f);
        }
    }

}


