using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class WaitingForPickup : StateBehaviour
{
    public float speed;
    public GameObject timeMaster;
    public Transform destination;
	// Called when the state is enabled
	void OnEnable () {
		Debug.Log("Started *State*");
	}
 
	// Called when the state is disabled
	void OnDisable () {
		Debug.Log("Stopped *State*");
	}
	
	// Update is called once per frame
	void Update () {
        Move(destination);
        CheckDay();
    }
    private void CheckDay()
    {
        if (timeMaster.GetComponent<Timers>().day)
        {
            SendEvent("TimeToPlay");
        }
    }
    private void Move(Transform destination)
    {

            Vector3 dir = Vector3.Normalize(destination.position - transform.position);
        Vector3 lerpDir = Vector3.Slerp(transform.position, dir, 4f);
        speed = timeMaster.GetComponent<Timers>().playerSpeed;
        if (Vector3.Distance(transform.position, destination.position) >= timeMaster.GetComponent<Timers>().kidMoveThershold)
        {
            transform.position += lerpDir * Time.deltaTime * speed;
            transform.rotation = Quaternion.LookRotation(lerpDir);
        }
    }
}


