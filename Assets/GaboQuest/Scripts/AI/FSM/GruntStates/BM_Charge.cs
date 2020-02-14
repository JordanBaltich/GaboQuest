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

    public float distanceToArrive;
    private float sqrDistanceToArrive;
    public float chargeDistance;

	// Called when the state is enabled
	void OnEnable () {
		Debug.Log("Started *Charge*");
        m_Grunt = GetComponent<Grunt>();
        m_Target = blackboard.GetGameObjectVar("Target");
        StartCoroutine(Charge());
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *Charge*");
        StopAllCoroutines();
	}

    void Setup()
    {
        sqrDistanceToArrive = distanceToArrive* distanceToArrive;
    }

    float sqrDistanceFromTarget()
    {
        return (m_Target.transform.position - transform.position).sqrMagnitude;
    }

    IEnumerator Charge()
    {
        Vector3 targetDestination = (m_Target.Value.transform.position - transform.position).normalized * chargeDistance;

        print("Charging");

        hitbox.SetActive(true);
        m_Grunt.ChargeToLocation(targetDestination);

        if(sqrDistanceFromTarget() < sqrDistanceToArrive)
        {
            hitbox.SetActive(false);
            SendEvent("ResumeChase");
        }

        yield return new WaitForSeconds(maxChargeTime);
        hitbox.SetActive(false);
        SendEvent("ResumeChase");
    }

}


