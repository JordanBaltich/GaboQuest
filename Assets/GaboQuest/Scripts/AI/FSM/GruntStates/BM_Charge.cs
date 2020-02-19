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
        hitbox.SetActive(true);

        Setup();
        StartCoroutine(Charge());
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *Charge*");
        StopAllCoroutines();

        hitbox.SetActive(false);
    }

    void Setup()
    {
        m_Grunt = GetComponent<Grunt>();
        m_Target = blackboard.GetGameObjectVar("Target");

        sqrDistanceToArrive = distanceToArrive * distanceToArrive;
    }

    float sqrDistanceFromTarget()
    {
        return (m_Target.transform.position - transform.position).sqrMagnitude;
    }

    IEnumerator Charge()
    {
        targetDestination = (m_Target.transform.position - transform.position).normalized;
        targetDestination = new Vector3(targetDestination.x * chargeDistance, 0, targetDestination.z * chargeDistance);
        targetDestination += transform.position;

        m_Grunt.ChargeToLocation(targetDestination);

        Debug.DrawLine(transform.position, targetDestination, Color.red);

        print(sqrDistanceFromTarget());

        yield return new WaitForSeconds(maxChargeTime);
        SendEvent("Idling");
    }
}


