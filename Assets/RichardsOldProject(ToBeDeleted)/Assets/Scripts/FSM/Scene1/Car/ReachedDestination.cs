using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class ReachedDestination : StateBehaviour
{
    public GameObject timeMaster;
    public Transform respawnPoint;
    public bool CarEmpty;

    public float timer;


    // Called when the state is enabled
    void OnEnable()
    {
        Debug.Log("Car Respawing");
        CarEmpty = timeMaster.GetComponent<Timers>().CarEmpty;
        if (timeMaster.GetComponent<Timers>().day)
        {
            timer = timeMaster.GetComponent<Timers>().carSpawnTime;
        }
        else
        {
            timer = timeMaster.GetComponent<Timers>().nightCycleTime;
        }

        transform.position = respawnPoint.position;
    }

    // Called when the state is disabled
    void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }

        if (timer <= 0)
        {

            if (timeMaster.GetComponent<Timers>().CarEmpty == true)
            {
                    SendEvent("RespawnEmpty");
            }

            if (timeMaster.GetComponent<Timers>().CarEmpty == false)
            {
                    SendEvent("RespawnLoaded");
            }
        }
    }
}


