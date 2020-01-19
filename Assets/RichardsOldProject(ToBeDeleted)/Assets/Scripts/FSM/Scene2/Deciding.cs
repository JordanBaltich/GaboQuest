using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class Deciding : StateBehaviour
{
    public TextToUI textObject;

    void Update ()
    {
	    if(gameObject.tag == "Sam")
        {
            SendEvent("MoveToPlayer");
        }

        if (gameObject.tag == "Gollum")
        {
            if (gameObject.GetComponent<Gollum_Hunger>().hungry)
            {
                textObject.GetComponent<TextToUI>().SetText("Gollum is hungry!");

                SendEvent("Hungry");
            }
            if (!gameObject.GetComponent<Gollum_Hunger>().hungry)
            {
                textObject.GetComponent<TextToUI>().SetText("heehee");

                SendEvent("NotHungry");
            }
        }
    }
}


