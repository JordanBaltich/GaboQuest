using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Charge : StateBehaviour
{
    public float chargeTime;
    public GameObject hitbox;
    Grunt m_Grunt;
    GameObjectVar m_Target;

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


    IEnumerator Charge()
    {
        Vector3 targetDestination = (m_Target.Value.transform.position - transform.position).normalized * chargeDistance;

        hitbox.SetActive(true);
        m_Grunt.ChargeToLocation(targetDestination);
        yield return new WaitForSeconds(chargeTime);
        hitbox.SetActive(false);
    }

}


