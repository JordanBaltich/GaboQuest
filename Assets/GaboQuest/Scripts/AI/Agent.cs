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

    GameObjectVar m_target;
    [SerializeField]
    GameObject Player;

    internal GameObject[] waypointTransform;
    internal int currentWaypointIndex = 0;
    bool incrementingWaypoint; //determines if next waypoint goes up or down the next on the array

    public bool cyclicPatrol;

    #region Status Variables

    internal IntVar health, stamina;
    internal BoolVar resting;

    #endregion Status Variables


    private void Awake()
    {
        AgentSetup();
    }

    void AgentSetup()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Blackboard>();
        stamina = blackboard.GetIntVar("Stamina");

        m_navAgent.speed = agentProperties.Speed;
        m_navAgent.angularSpeed = agentProperties.AngularSpeed;
        m_navAgent.acceleration = agentProperties.Acceleration;

        m_target = blackboard.GetGameObjectVar("Target");

        Player = GameObject.Find("Player");
        
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

    internal void setPlayerAsTarget()
    {
        m_target = Player;
    }
}
