using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class s_CheckHealth : StateBehaviour
{
    public Sam_Health Sam_Health;

	// Update is called once per frame
	void Update () {
	
        if (!Sam_Health.healthy)
        {
            SendEvent("NotRecovered");
        }
        if (Sam_Health.healthy)
        {
            SendEvent("Recovered");
        }
    }

}


