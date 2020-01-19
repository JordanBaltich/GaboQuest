using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using UnityEngine.AI;


public class g_ChasingButterfly : StateBehaviour
{
    public Vision vision;

    public GameObject Waypoint;
    NavMeshAgent navMeshAgent;

    public TextToUI textObject;


    // Called when the state is enabled
    void OnEnable()
    {
        vision = vision.GetComponent<Vision>();

        navMeshAgent = GetComponent<NavMeshAgent>();

    }


    // Update is called once per frame
    void Update()
    {
        if (vision.visibleObjects.Count > 0)
        {
            Waypoint.transform.position = vision.visibleObjects[0].transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Butterfly")
        {
            other.gameObject.SetActive(false);
            textObject.GetComponent<TextToUI>().SetText(textObject.g_catching);

            SendEvent("CaughtButterfly");
        }
    }

}


