using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;



public class g_Searching : StateBehaviour
{
    public Vision vision;
    public GameObject Waypoint;
    public GameObject ButterflyWaypoint;

    public TextToUI textObject;

    // Called when the state is enabled
    void OnEnable () {
        vision = vision.GetComponent<Vision>();
        Waypoint.transform.position = ButterflyWaypoint.transform.position;
        textObject.GetComponent<TextToUI>().SetText(textObject.g_chasing);

    }


    // Update is called once per frame
    void Update()
    {
        if (vision.visibleObjects.Count > 0)
        {
            SendEvent("FoundButterfly");
        }
    }
}


