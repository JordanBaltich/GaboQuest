using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class KidRunning : StateBehaviour
{
    public Transform PlayPoint;
    public GameObject timeMaster;
    public float radius;
    public float rotationSpeed;
    public float distanceThreshold;
    public float time;
    public float moveSpeed;
    public float caughtTime = 1f;
    public bool caught = false;
    public Vector3 circleDir;
    public Vector3 distanceDir;
    public Vector3 finalPosition;
    // Called when the state is enabled
    void OnEnable()
    {
        caught = false;
        caughtTime = 1f;

    }

    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.CompareTag("Kid"))
            {
                caught = true;
                
            }
        
    }
    public void CaughtTimer()
    {
        if (caught)
        {
            caughtTime -= Time.deltaTime;
            if (caughtTime <= 0)
            {
                SendEvent("TimetoCatch");
            }

        }
    }


    // Update is called once per frame

    void Update()
    {
        Play(PlayPoint.position);
        //CaughtTimer();
        //CheckDay();
    }

    public void CheckDay()
    {
        if (!timeMaster.GetComponent<Timers>().day)
        {
            SendEvent("TimeToLeave");
        }
    }
    public void Play(Vector3 point)
    {

        //float distanceToPlaypoint = Vector3.Distance(transform.position, point);
        Vector3 dir = Vector3.Normalize(point - transform.position);
        time += Time.deltaTime;
        //moveSpeed = timeMaster.GetComponent<Timers>().playerSpeed;
        radius = timeMaster.GetComponent<Timers>().kid2Radius ;
        //rotationSpeed = timeMaster.GetComponent<Timers>().kid2RotationSpeed;
        circleDir = new Vector3(Mathf.Sin(Time.time), 0f, Mathf.Cos(Time.time));
        distanceDir = new Vector3(dir.x, 0f, dir.z);
        if (!caught)
        {
            //if (distanceToPlaypoint <= timeMaster.GetComponent<Timers>().kid2DistanceThreshold)
            //{
                
                finalPosition = (point + (circleDir * radius)* moveSpeed) * Time.deltaTime ;
                transform.position = finalPosition;
                transform.rotation = Quaternion.LookRotation(-circleDir);
            //print(point);
           // }
            //else
            //{
            //    transform.position += distanceDir * moveSpeed * Time.deltaTime;
            //    transform.rotation = Quaternion.LookRotation(dir);
           // }
        }
    }

}


