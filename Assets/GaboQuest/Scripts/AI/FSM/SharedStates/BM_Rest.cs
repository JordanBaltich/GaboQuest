using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Rest : StateBehaviour
{
    Agent m_agent;

    public float IdleMinTime;
    public Vector2 IdleTimeAddRange;
    public float idleTime;

    // Called when the state is enabled
    void OnEnable () {
		Debug.Log("Started *State*");

        m_agent = GetComponent<Agent>();

        StartCoroutine(StartIdling());
	}


    // Called when the state is disabled
    void OnDisable () {
		Debug.Log("Stopped *State*");
	}
	
    void DetermineIdleTime()
    {
        if (idleTime == 0)
            idleTime = IdleMinTime + Random.Range(IdleTimeAddRange.x, IdleTimeAddRange.y);
        else
            return;
    }

    private IEnumerator StartIdling()
    {
        DetermineIdleTime();

        m_agent.m_navAgent.isStopped = true;
        yield return new WaitForSeconds(idleTime);
        m_agent.m_navAgent.isStopped = false;

        SendEvent("ResumePatrol");
        SendEvent("ResumeWandering");
    }
}


