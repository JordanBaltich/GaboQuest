using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using UnityEngine.AI;

public class s_Limping : StateBehaviour
{
    public TextToUI textObject;
    NavMeshAgent navMeshAgent;

    // Called when the state is enabled
    void OnEnable () {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 3.5f;

        textObject.GetComponent<TextToUI>().SetText(textObject.s_limping);

    }
    private void Update()
    {
        SendEvent("Slow");

    }
}


