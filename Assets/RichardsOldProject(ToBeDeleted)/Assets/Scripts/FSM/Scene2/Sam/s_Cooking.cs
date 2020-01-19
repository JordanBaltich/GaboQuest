using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class s_Cooking : StateBehaviour
{
    public TextToUI textObject;
    public GameObject Pot;

    public float timer;
    // Called when the state is enabled
    void OnEnable () {
       
        timer = 0f;
    }


	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = Pot.transform.position;
        gameObject.transform.rotation = Pot.transform.rotation;
        textObject.GetComponent<TextToUI>().SetText(textObject.s_cooking);
        timer += Time.deltaTime;
        if(timer > 3f)
        {
            SendEvent("DoneCooking");
        }
	}
}


