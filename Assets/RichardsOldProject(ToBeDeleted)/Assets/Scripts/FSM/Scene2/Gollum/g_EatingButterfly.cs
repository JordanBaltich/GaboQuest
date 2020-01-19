using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class g_EatingButterfly : StateBehaviour
{
    public TextToUI textObject;

    // Called when the state is enabled
    void OnEnable () {
        EatButterfly();
    }
 
	
	// Update is called once per frame
	void Update () {
 
    }

    void EatButterfly()
    {
        Invoke("DoneEating", 2f);
    }
    void DoneEating()
    {
        gameObject.GetComponent<Gollum_Hunger>().hunger += 1;
        textObject.GetComponent<TextToUI>().SetText(textObject.g_catching);

        SendEvent("AteButterfly");
    }
}


