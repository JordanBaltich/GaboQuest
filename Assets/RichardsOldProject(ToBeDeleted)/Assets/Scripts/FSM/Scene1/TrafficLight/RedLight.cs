using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class RedLight : StateBehaviour
{
    public GameObject redLight;
    public GameObject timemaster;

    public float timer;


	void OnEnable () {
		Debug.Log("Red Light On");
        timer = timemaster.GetComponent<Timers>().redLightTime;
        if (!redLight.activeSelf) { redLight.SetActive(true); }
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Red Light Off");

        if (redLight.activeSelf) { redLight.SetActive(false); }
    }
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }
        if (timer <= 0)
        {
            SendEvent("TurningGreen");


        }


    }
}


