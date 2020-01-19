using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class CheckingDestination : StateBehaviour
{

    public TextToUI textObject;

    // Update is called once per frame
    void Update () {


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Sam")
        {
            if (collision.gameObject.tag=="Pot")
            {
                SendEvent("CookingPot");
            }
        }
        if (gameObject.tag == "Gollum")
        {
            if (collision.gameObject.tag == "Artifact")
            { 

                textObject.GetComponent<TextToUI>().SetText(textObject.g_artifact);
                SendEvent("GotArtifact");
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "SamWaypoint")
        {
            if (gameObject.tag == "Sam")
            {
                //textObject.GetComponent<TextToUI>().SetText("??");

                SendEvent("Null");

            }
        }
        if (other.gameObject.tag == "GollumWaypoint")
        {
            if (gameObject.tag == "Gollum")
            {
                //textObject.GetComponent<TextToUI>().SetText("??");

                SendEvent("Null");

            }
        }
    }

}


