using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class s_Resting : StateBehaviour
{
    public GameObject Pot;
    public Pot_Pickup RawFood;

    public TextToUI textObject;


    // Called when the state is enabled
    void OnEnable () {
        gameObject.transform.position = Pot.transform.position;
        gameObject.transform.rotation = Pot.transform.rotation;
        textObject.GetComponent<TextToUI>().SetText(textObject.s_resting);
    }

	// Update is called once per frame
	void Update () {
        gameObject.transform.position = Pot.transform.position;
        gameObject.transform.rotation = Pot.transform.rotation;
        CheckFood();
    }
    void CheckFood()
    {
        if (RawFood.GetComponent<Pot_Pickup>().rawFood == true)
        {
            SendEvent("ReceivedFood");
        }
    }
}


