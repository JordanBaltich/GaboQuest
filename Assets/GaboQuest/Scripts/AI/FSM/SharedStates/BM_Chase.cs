﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Chase : StateBehaviour
{
    Agent m_agent;

    GameObjectVar target;
    Vector3Var destination;

    [SerializeField]
    private float ticks = .2f;

    float sqrDistanceToTarget;

    public float distanceToReachTarget = 1.5f;
    float sqrDistanceToReachTarget;

	// Called when the state is enabled
	void OnEnable () {
		Debug.Log("Started Chase");

        m_agent = GetComponent<Agent>();

        Setup();

        SetAgentSpeed();

        StartCoroutine(StartChasing());
	}

    // Called when the state is disabled
    void OnDisable () {
		Debug.Log("Stopped Chase");
        StopAllCoroutines();
    }

    //sets up all references and proper metrics
    void Setup()
    {
        m_agent = GetComponent<Agent>();
        target = blackboard.GetGameObjectVar("Target");
        destination = blackboard.GetVector3Var("Destination");
        sqrDistanceToReachTarget = distanceToReachTarget * distanceToReachTarget;

    }


    //Changes agent properties to run from walk
    private void SetAgentSpeed()
    {
        m_agent.m_navAgent.speed = m_agent.agentProperties.RunSpeed;
        m_agent.m_navAgent.angularSpeed = m_agent.agentProperties.RunAngularSpeed;
        m_agent.m_navAgent.acceleration = m_agent.agentProperties.RunAcceleration;
    }

    //Calculate the distance from target to player position
    void calculateDistanceFromTarget()
    {
        sqrDistanceToTarget = (target.transform.position - transform.position).sqrMagnitude;
    }

    //Begins chasing the player
    IEnumerator StartChasing()
    {
        while (enabled)
        {
            //sets the destination for the agent to target the player's position
            if (target.Value != null)
            {
                calculateDistanceFromTarget();
                destination.Value = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                m_agent.m_navAgent.SetDestination(destination.Value);
            }


            //changes the state of the agent once it reaches a certain distance to the target
            if (sqrDistanceToTarget < sqrDistanceToReachTarget)
            {
                SendEvent("ReachedTarget");
                yield return null;
            }

            yield return new WaitForSeconds(ticks);
        }
    }
}


