using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using System;

public class BM_Patrol : StateBehaviour
{
    PatrolPoints m_patrolpoints;

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
        m_patrolpoints.WalkToWaypoint();

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
        m_patrolpoints = GetComponent<PatrolPoints>();
        target = blackboard.GetGameObjectVar("Threat");
        destination = blackboard.GetVector3Var("Destination");

        targetWaypoint = blackboard.GetGameObjectVar("TargetWaypoint");
        targetWaypoint.Value = m_patrolpoints.waypoints[m_patrolpoints.waypointIndex];
        destination.Value = targetDestination();

        sqrDistanceToArrive = distanceToArrive * distanceToArrive;

        m_patrolpoints.ResetAgent();

        updateDistanceToDestination();
    }

    private void updateDistanceToDestination()
    {
        sqrDistanceToDestination = (targetDestination() - transform.position).sqrMagnitude;
    }

    private Vector3 targetDestination()
    {
        return targetWaypoint.Value.transform.position;
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
            print(sqrDistanceToDestination);

            //increment waypoint if distance is reached
            if (sqrDistanceToDestination < sqrDistanceToArrive)
            {
                //cyclic patrol
                m_patrolpoints.waypointIndex = (m_patrolpoints.waypointIndex + 1) % m_patrolpoints.waypoints.Count;
                destination.Value = targetDestination();

                SendEvent("Idling");

                yield return null;
            }
            updateDistanceToDestination();

            yield return new WaitForSeconds(.2f);
        }
    }

}


