using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using UnityEngine.AI;


public class s_WaitingForPickup : StateBehaviour
{
    public TextToUI textObject;
    public GameObject Food;
    public GameObject Pot;
    NavMeshAgent navMeshAgent;

    public GameObject Player;

    // Called when the state is enabled
    void OnEnable () {
        textObject.GetComponent<TextToUI>().SetText(textObject.s_waiting);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 8f;
        Food.SetActive(true);
        gameObject.GetComponent<Sam_Health>().health = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Pot.transform.position;
        gameObject.transform.rotation = Pot.transform.rotation;
        if (Player.GetComponent<ItemPickup>().food)
        {
            SendEvent("TookFood");
        }
    }

}


