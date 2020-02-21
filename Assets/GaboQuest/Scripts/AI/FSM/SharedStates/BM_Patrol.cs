using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using System;

public class BM_Patrol : StateBehaviour
{
    PatrolPoints m_patrolpoints;
    Agent m_agent;

    float sqrDistanceToDestination;
    public float distanceToArrive = 1.3f;
    float sqrDistanceToArrive;

    Vector3Var destination;

    GameObjectVar target;

    GameObjectVar targetWaypoint;

    // Called when the state is enabled
    void OnEnable () {
		Debug.Log("Started Patrol");

        Setup();
        StartCoroutine(CheckForEnemy());
        StartCoroutine(CheckDistanceFromWaypoint());
        
    }

    

    // Called when the state is disabled
    void OnDisable () {
		Debug.Log("Stopped Patrol");
        StopAllCoroutines();
	}

    // sets up all references and proper metrics
    private void Setup()
    {
        m_patrolpoints = GetComponent<PatrolPoints>();
        m_agent = GetComponent<Agent>();
        target = blackboard.GetGameObjectVar("Target");
        destination = blackboard.GetVector3Var("Destination");

        targetWaypoint = blackboard.GetGameObjectVar("TargetWaypoint");
        targetWaypoint.Value = m_patrolpoints.waypoints[m_patrolpoints.waypointIndex];
        destination.Value = targetDestination();

        sqrDistanceToArrive = distanceToArrive * distanceToArrive;

        m_patrolpoints.ResetAgent();

        UpdateDistanceToDestination();
    }

    // calculates the distance from this to target
    private void UpdateDistanceToDestination()
    {
        sqrDistanceToDestination = (targetDestination() - transform.position).sqrMagnitude;
    }

    // calculates the distance from this to target
    private Vector3 targetDestination()
    {
        return targetWaypoint.Value.transform.position;
    }

    // checks if agent has vision of the enemy, and changes the state
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
            m_agent.m_navAgent.SetDestination(destination);

            //increment waypoint if distance is reached
            if (sqrDistanceToDestination < sqrDistanceToArrive)
            {
                //cyclic patrol
                m_patrolpoints.waypointIndex = (m_patrolpoints.waypointIndex + 1) % m_patrolpoints.waypoints.Count;
                destination.Value = targetDestination();

                SendEvent("Idling");

                yield return null;
            }
            UpdateDistanceToDestination();

            yield return new WaitForSeconds(.2f);
        }
    }
}