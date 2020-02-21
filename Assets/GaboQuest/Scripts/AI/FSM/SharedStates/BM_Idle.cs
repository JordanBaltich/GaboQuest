using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Idle : StateBehaviour
{
    Agent m_agent;
    GameObjectVar m_Target;

    // Called when the state is enabled
    void OnEnable()
    {
        Debug.Log("Started *Idle*");

        m_agent = GetComponent<Agent>();

        StartCoroutine(StartIdling());
    }


    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Stopped *Idle*");
    }

    //Starts the idling state
    private IEnumerator StartIdling()
    {
        
        m_agent.m_navAgent.isStopped = true;
        yield return new WaitForSeconds(m_agent.agentProperties.PatrolWait);
        m_agent.m_navAgent.isStopped = false;

        if (m_Target != null)
            SendEvent("ResumeChase");
        else
            SendEvent("ResumePatrol");       
    }
}


