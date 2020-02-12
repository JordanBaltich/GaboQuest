using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Stunned : StateBehaviour
{
    public float StunTime;

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
	
    IEnumerator StartStun()
    {
        yield return new WaitForSeconds(StunTime);
        SendEvent("ResumeChase");
    }

}


