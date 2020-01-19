using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class Arriving : StateBehaviour
{
    public Transform destination;
    public Transform SpawnPoint;
    public Transform DropoffPoint;
    public Transform nextPoint;
    public Transform kidSpawnPoint;
    public bool didAction;

    public bool travelDirection;
    public Vector3 dir;
    public float speed;
    public float kidOffset;
    public float positionThreshold;

    public GameObject timeMaster;
    public GameObject kid3;

    void OnEnable()
    {
        didAction = false;
        kidOffset = timeMaster.GetComponent<Timers>().kidOffset;
        speed = timeMaster.GetComponent<Timers>().carSpeed;
        positionThreshold = timeMaster.GetComponent<Timers>().carDespawnThreshold;
    }

    // Called when the state is disabled
    void OnDisable()
    {
        //CarEmpty = false;

        Debug.Log("Stopped *State*");
    }

    // Update is called once per frame
    void Update()
    {
        Move(TravelDir(), DropoffPoint);
        CheckDestination(DropoffPoint);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Red") || collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            SendEvent("Blocked");
        }
        if (collision.gameObject.CompareTag("Kid"))
        {

            timeMaster.GetComponent<Timers>().CarEmpty = false;

            SendEvent("Loaded");
        }
    }
    public float TravelDir()
    {
        if (dir.x >= 0)
        {
            return 1;
        }
        else return -1;
    }
    private void Move(float dirModifier, Transform destination)
    {

        Vector3 dir = Vector3.Normalize(destination.position - transform.position);
        Vector3 lerpDir = Vector3.Slerp(transform.position, dir, 4f);
        speed = timeMaster.GetComponent<Timers>().carSpeed;

        transform.position += lerpDir * Time.deltaTime * speed * dirModifier;
        transform.rotation = Quaternion.LookRotation(lerpDir);

    }
    //Change Destination
    public Transform NextDestination()
    {
        //if ((nextPoint = destination) && timeMaster.GetComponent<Timers>().CarEmpty==true)
        //{
        //    nextPoint = destination;
            
        //} else return DropoffPoint;

        //if ((nextPoint = DropoffPoint) && timeMaster.GetComponent<Timers>().CarEmpty == false)
        //{
        //    nextPoint = destination;

        //}

        return DropoffPoint;
        
    }
    //Check Arrived
    private void CheckDestination(Transform t)
    {
        if (Vector3.Distance(transform.position, t.position) <= positionThreshold)
        {

            if (timeMaster.GetComponent<Timers>().CarEmpty == true && !didAction)
            {
                if (!timeMaster.GetComponent<Timers>().day)
                {
                    SendEvent("WaitKid");
                }else SendEvent("Leave");
            }
            if (timeMaster.GetComponent<Timers>().CarEmpty == false)
            {

                if (timeMaster.GetComponent<Timers>().day)
                {

                    SpawnKid();
                    timeMaster.GetComponent<Timers>().CarEmpty = true;
                    SendEvent("Leave");
                }
                //didAction = true;
            }

                

            //if (nextPoint = destination)
            //{
            //    SendEvent("Despawn");
            //}
        }
    }
    private void SpawnKid()
    {
        Debug.Log("Dropoff Kid");
        didAction = true;
        Vector3 spawnOffset = new Vector3(DropoffPoint.transform.position.x, DropoffPoint.transform.position.y, DropoffPoint.transform.position.z + kidOffset);
        Instantiate(kid3, spawnOffset, Quaternion.identity);
    }
}