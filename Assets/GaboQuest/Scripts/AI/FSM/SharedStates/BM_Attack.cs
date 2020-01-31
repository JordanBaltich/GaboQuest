﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class BM_Attack : StateBehaviour
{
	// Called when the state is enabled
	void OnEnable () {
		Debug.Log("Started Attack");
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped Attack");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


