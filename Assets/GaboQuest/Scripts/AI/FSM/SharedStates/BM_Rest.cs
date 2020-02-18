using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using System;

public class BM_Rest : StateBehaviour
{
    Agent m_agent;

    IntVar stamina;

    void OnEnable()
    {
        Debug.Log("Started *Rest*");

        Setup();
        m_agent.m_navAgent.isStopped = true;
        StartCoroutine(RegenerateStamina());
    }

    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Stopped *Rest*");

        StopAllCoroutines();
        m_agent.m_navAgent.isStopped = false;

        m_agent.resting.Value = false;
    }

    void Setup()
    {
        m_agent = GetComponent<Agent>();
        stamina = blackboard.GetIntVar("Stamina");

        m_agent.resting.Value = true;
    }

    private IEnumerator RegenerateStamina()
    {
        while (enabled)
        {
            if (stamina.Value >= 100)
                break;

            stamina.Value++;

            yield return new WaitForSeconds(m_agent.agentProperties.StaminaRegenerationRate);
        }
        SendEvent("RegainedEnergy");
    }


}


