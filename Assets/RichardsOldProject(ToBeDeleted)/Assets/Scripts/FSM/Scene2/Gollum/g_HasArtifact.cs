using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class g_HasArtifact : StateBehaviour
{
    public TextToUI textObject;

    public GameObject Waypoint;

    public GameObject Bargainpoint;


    // Called when the state is enabled
    void OnEnable ()
    {

        textObject.GetComponent<TextToUI>().SetText("The Artifact is mine!");
    }

    // Called when the state is disabled
    void OnDisable () {
		Debug.Log("Stopped *State*");
	}
	
	// Update is called once per frame
	void Update ()
    {

        Waypoint.transform.position = Bargainpoint.transform.position;
        Waypoint.transform.rotation = Bargainpoint.transform.rotation;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Red")
        {
            SendEvent("Bargining");
        }
    }

}


