using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using UnityEngine.AI;

public class CheckingRoute : StateBehaviour
{
    public GameObject Waypoint;
    public TextToUI textObject;

    NavMeshAgent navMeshAgent;

    void OnEnable()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Called when the state is disabled
    void OnDisable()
    { 
        navMeshAgent.stoppingDistance = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        PlaceWaypoint();
        if (gameObject.tag == "Gollum")
        {
            textObject.GetComponent<TextToUI>().SetText("Yes?");

        }
    }

    void PlaceWaypoint()
    {


        if (Input.GetMouseButtonDown(1))
        {
            if (Waypoint != null)
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Waypoint.gameObject.transform.position = hit.point;
                    print(Waypoint.name + " position is now " + Waypoint.gameObject.transform.position);
                    SendEvent("Reached");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SendEvent("PlayerInput");
        }
        if (gameObject.tag == "Sam")
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SendEvent("PlayerInput");
            }
        }
        if (gameObject.tag == "Gollum")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SendEvent("PlayerInput");
            }
        }
    }
}


