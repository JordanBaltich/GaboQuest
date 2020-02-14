using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Attack : StateBehaviour
{
    Agent m_agent;
    GameObjectVar Target;

    // Called when the state is enabled
    void OnEnable () {
		Debug.Log("Started Attack");
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped Attack");
	}
	

    void Setup()
    {

    }

    void DamageTarget(GameObject DamageableObject)
    {
        ITakeDamage Damageable = DamageableObject.GetComponent<ITakeDamage>();
        
        Damageable?.TakeDamage(1);
    }
}


