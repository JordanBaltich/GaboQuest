using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class Obstructed : StateBehaviour
{

    // Called when the state is enabled
    void OnEnable () {


        Debug.Log("Blocked");
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *State*");
	}
	
	// Update is called once per frame
	void Update () {
        //SendEvent("Cleared");

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag ( "Green"))
        {
            Debug.Log("Cleared");

            SendEvent("Cleared");
        }
    }
    private IEnumerator OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            yield return new WaitForSeconds(2f);

            Debug.Log("Cleared");

            SendEvent("Cleared");
        }

    }

}


