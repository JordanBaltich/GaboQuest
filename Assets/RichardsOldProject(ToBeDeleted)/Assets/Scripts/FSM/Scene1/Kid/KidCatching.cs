using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class KidCatching : StateBehaviour
{
    public float speed;
    public GameObject timeMaster;
    public Transform destination;
    public GameObject kid;
    public float catchCD;

	// Called when the state is enabled
	void OnEnable () {
        catchCD = timeMaster.GetComponent<Timers>().catchCD;
        timeMaster = GameObject.Find("TimeMaster");
        kid = GameObject.Find("Kid2");
    }

    // Update is called once per frame
    void Update () {
        Move(kid.transform);
        CheckDay();
    }
    public void CheckDay()
    {
        if (!timeMaster.GetComponent<Timers>().day)
        {
            SendEvent("TimeToLeave");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kid"))
        {
            SendEvent("TimeToRun");
        }
    }
    private void Move(Transform destination)
    {
        catchCD -= Time.deltaTime;
        
        Vector3 dir = Vector3.Normalize(destination.position - transform.position);
        Vector3 lerpDir = Vector3.Slerp(transform.position, dir, 4f);
        speed = timeMaster.GetComponent<Timers>().playerSpeed;
        if (catchCD <= 0)
        {
            transform.position += lerpDir * (Time.deltaTime * Random.value) * speed;
            transform.rotation = Quaternion.LookRotation(lerpDir);
        }
    }
}


