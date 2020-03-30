using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_ReTarget : StateBehaviour
{
    Agent m_agent;
    GameObjectVar m_target;
    [SerializeField]
    float rotationSpeed;

    float angleFromTarget;

    [SerializeField]
    float angleToAct = 15f;

    [SerializeField]
    float TimeBeforeActing = 1f;

	// Called when the state is enabled
	void OnEnable () {
		Debug.Log("Started *LockOn*");
        Setup();
    }
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *LockOn*");
	}
	
	void Setup()
    {
        m_target = blackboard.GetGameObjectVar("Target");
        m_agent = GetComponent<Agent>();
    }

    private void Update()
    {
        if(m_target == null)
        {
            m_target = blackboard.GetGameObjectVar("Target");

        }
        Vector3 targetDirection = m_target.transform.position - transform.position;
        angleFromTarget = Vector3.Angle(targetDirection, transform.forward);

        float singleStep = rotationSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        if(angleFromTarget < angleToAct)
        {
            StartCoroutine(TransitionToCharge());
        }
    }

    IEnumerator TransitionToCharge()
    {
        m_agent.m_navAgent.isStopped = true;
        yield return new WaitForSeconds(TimeBeforeActing);
        m_agent.m_navAgent.isStopped = false;
        SendEvent("LockedOn");
    }
}


