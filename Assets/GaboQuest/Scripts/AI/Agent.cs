using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourMachine;

public class Agent : MonoBehaviour
{
    public AgentProperties agentProperties;
    internal Blackboard blackboard;
    internal NavMeshAgent m_navAgent;

    internal GameObject[] waypointTransform;
    internal int currentWaypointIndex = 0;
    bool incrementingWaypoint; //determines if next waypoint goes up or down the next on the array

    public bool cyclicPatrol;

    #region Status Variables

    internal IntVar stamina, focus;
    internal BoolVar resting, distracted;

    internal float staminaDepletionFrequency, focusDepletionFrequency, staminaRegainFrequency, focusRegainFrequency;

    #endregion Status Variables


    private void Awake()
    {
        AgentSetup();
        StartCoroutine(DepleteEnergy());
    }

    void AgentSetup()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Blackboard>();
        stamina = blackboard.GetIntVar("Stamina");
        distracted = blackboard.GetBoolVar("Distracted");

        m_navAgent.speed = agentProperties.Speed;
        m_navAgent.angularSpeed = agentProperties.AngularSpeed;
        m_navAgent.acceleration = agentProperties.Acceleration;

    }

    void setNextTargetDestination()
    {
        if (cyclicPatrol)
        {
            if (currentWaypointIndex == waypointTransform.Length)
                currentWaypointIndex = 0;
            else
                currentWaypointIndex++;
        }
        else
        {
            if (incrementingWaypoint)
            {
                currentWaypointIndex++;

                if (currentWaypointIndex == waypointTransform.Length)
                    incrementingWaypoint = false;
            }
            else if (!incrementingWaypoint)
            {
                currentWaypointIndex--;

                if (currentWaypointIndex == 0)
                    incrementingWaypoint = true;
            }
        }

        m_navAgent.SetDestination(waypointTransform[currentWaypointIndex].transform.position);
    }

    public void GoToWaypoint()
    {
        m_navAgent.SetDestination(waypointTransform[currentWaypointIndex].transform.position);
    }

    internal void ResetAgent()
    {
        m_navAgent.speed = agentProperties.Speed;
        m_navAgent.angularSpeed = agentProperties.AngularSpeed;
        m_navAgent.acceleration = agentProperties.Acceleration;
    }

    internal void SetRunAgentSpeed()
    {
        m_navAgent.speed = agentProperties.RunSpeed;
        m_navAgent.angularSpeed = agentProperties.RunAngularSpeed;
        m_navAgent.acceleration = agentProperties.RunAcceleration;
    }


    IEnumerator DepleteEnergy()
    {
        while (true)
        {
            if (stamina.Value > 0 && !resting)
            {
                stamina.Value--;
                yield return new WaitForSeconds(staminaDepletionFrequency);
            }
            else
            {
                blackboard.SendEvent("Tired");
            }
            yield return null;
        }
    }
}
