using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class WaitingForKid : StateBehaviour
{
    public GameObject timeMaster;

    void OnEnable()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kid"))
        {
            
            timeMaster.GetComponent<Timers>().CarEmpty = false;
            //yield return new WaitForSeconds(2f);
            SendEvent("Loaded");
        }

    }

}


