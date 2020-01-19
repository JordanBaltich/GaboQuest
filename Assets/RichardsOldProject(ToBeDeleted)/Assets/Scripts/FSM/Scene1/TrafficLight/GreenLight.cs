using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class GreenLight : StateBehaviour
{
    public GameObject greenLight;
    public GameObject timemaster;

    public float timer;


    void OnEnable()
    {
        Debug.Log("Green Light On");
        timer = timemaster.GetComponent<Timers>().redLightTime;

        if (!greenLight.activeSelf) { greenLight.SetActive(true); }
    }

    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Green Light Off");

        if (greenLight.activeSelf) { greenLight.SetActive(false); }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != 0)
        {
            timer -= 1 * Time.deltaTime;
        }
        if (timer <= 0)
        {
            SendEvent("TurningRed");

        }


    }
}

