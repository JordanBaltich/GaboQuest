using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class Sunset : StateBehaviour
{
    public Light light;
    public GameObject timeMaster;
    float timer;

    // Called when the state is enabled
    void OnEnable () {
        light = GetComponent<Light>();
        timer = timeMaster.GetComponent<Timers>().nightCycleTime;

        Debug.Log("Started *State*");
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *State*");
	}
	
	// Update is called once per frame
	void Update () {

            light.intensity -= 1f * Time.deltaTime;

        if (light.intensity <= 0f)
        {
            timer -= Time.deltaTime;
            timeMaster.GetComponent<Timers>().day = false;
            if (timer <= 0)
            {
                
                SendEvent("SunUp");
            }
        }

    }
}


