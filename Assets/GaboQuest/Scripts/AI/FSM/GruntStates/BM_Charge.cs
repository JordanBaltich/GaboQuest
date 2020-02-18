using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Charge : StateBehaviour
{
    public float maxChargeTime;
    [SerializeField]
    private GameObject hitbox;
    Grunt m_Grunt;
    GameObjectVar m_Target;

    Vector3 targetDestination;

    public float distanceToArrive;
    private float sqrDistanceToArrive;
    public float chargeDistance;

	// Called when the state is enabled
	void OnEnable () {
		Debug.Log("Started *Charge*");
        Setup();

        StartCoroutine(Charge());
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *Charge*");
        StopAllCoroutines();
	}

    void Setup()
    {
        m_Grunt = GetComponent<Grunt>();
        m_Target = blackboard.GetGameObjectVar("Target");

        sqrDistanceToArrive = distanceToArrive* distanceToArrive;
    }

    float sqrDistanceFromTarget()
    {
        return (m_Target.transform.position - transform.position).sqrMagnitude;
    }

    IEnumerator Charge()
    {
        hitbox.SetActive(true);

        targetDestination = (m_Target.transform.position - transform.position).normalized * chargeDistance;

        m_Grunt.ChargeToLocation(targetDestination);


        print(sqrDistanceFromTarget());

        if (sqrDistanceFromTarget() < sqrDistanceToArrive)
        {
            hitbox.SetActive(false);
            SendEvent("ResumeChase");
        }

        yield return new WaitForSeconds(maxChargeTime);
        hitbox.SetActive(false);
        SendEvent("Idling");
    }
}


