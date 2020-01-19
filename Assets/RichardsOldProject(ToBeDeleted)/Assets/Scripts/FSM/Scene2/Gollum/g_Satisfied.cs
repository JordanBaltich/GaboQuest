using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class g_Satisfied : StateBehaviour
{
    public TextToUI textObject;
    public float timer;
    // Called when the state is enabled
    void OnEnable () {
        textObject.GetComponent<TextToUI>().SetText(textObject.g_satisfied);
        gameObject.GetComponent<Gollum_Hunger>().cookedFood = true;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > 2f)
        {
            SendEvent("Dealt");
        }

	}
}


