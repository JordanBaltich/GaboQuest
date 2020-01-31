using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Chase : StateBehaviour
{
	// Called when the state is enabled
	void OnEnable () {
		Debug.Log("Started Chase");
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped Chase");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


