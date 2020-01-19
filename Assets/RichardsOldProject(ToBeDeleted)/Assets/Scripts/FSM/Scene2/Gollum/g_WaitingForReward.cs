using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class g_WaitingForReward : StateBehaviour
{
    public TextToUI textObject;

    public GameObject Waypoint;

    public GameObject Bargainpoint;

    // Called when the state is enabled
    void OnEnable ()
    {
        textObject.GetComponent<TextToUI>().SetText(textObject.g_bargaining);
    }

    void Update()
    { 
        gameObject.transform.position = Bargainpoint.transform.position;
        gameObject.transform.rotation = Bargainpoint.transform.rotation;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<ItemPickup>().food == true)
            {
                SendEvent("Bribed");
            } else
            {
            textObject.GetComponent<TextToUI>().SetText(textObject.g_bargaining);
            }

        }
    }
}


