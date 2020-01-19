using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class Sunrise : StateBehaviour
{
    public Light light;
    public GameObject timeMaster;
    float timer;
    // Called when the state is enabled
    void OnEnable () {
        light = GetComponent<Light>();
        timer = timeMaster.GetComponent<Timers>().dayCycleTime;
        Debug.Log("Started *State*");
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *State*");
	}

    // Update is called once per frame
    void Update() {
        if (light.intensity<=1.4f)
        {
            light.intensity += 1f * Time.deltaTime;
        }
        if (light.intensity >= 1.4f)
        {
            timer -= Time.deltaTime;
            timeMaster.GetComponent<Timers>().day = true;
            if (timer <= 0)
            {
                
                SendEvent("SunDown");
            }
        }

    }
}


