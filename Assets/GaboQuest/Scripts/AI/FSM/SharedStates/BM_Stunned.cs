using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Stunned : StateBehaviour
{
    public float StunTime;
    Agent m_Agent;
    GameObjectVar m_target;

    // Called when the state is enabled
    void OnEnable () {
		Debug.Log("Started *Stunned*");
        StartCoroutine(StartStun());
    }
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *Stunned*");
        StopAllCoroutines();
	}
	
    void Setup()
    {
        m_Agent = GetComponent<Agent>();
        m_target = blackboard.GetGameObjectVar("Target");
    }

    IEnumerator StartStun()
    {
        m_Agent.m_navAgent.isStopped = true;
        yield return new WaitForSeconds(StunTime);
        m_Agent.m_navAgent.isStopped = false;

        if (m_target.Value == null)
            m_Agent.setPlayerAsTarget();

        SendEvent("ResumeChase");
    }
}


