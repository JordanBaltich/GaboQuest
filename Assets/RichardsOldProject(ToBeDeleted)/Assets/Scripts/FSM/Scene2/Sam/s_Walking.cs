using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using UnityEngine.AI;

public class s_Walking : StateBehaviour
{

    public TextToUI textObject;

    // Called when the state is enabled
    void OnEnable()
    {

        textObject.GetComponent<TextToUI>().SetText("Let's go Mr Frodo");

    }
    void Update()
    {
        SendEvent("Fast");
    }
}


