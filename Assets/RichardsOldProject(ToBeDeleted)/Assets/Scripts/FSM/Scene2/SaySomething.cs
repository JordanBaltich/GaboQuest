using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;

public class SaySomething : StateBehaviour
{
    public TextToUI textObject;
    public float timer;
    public bool reached;


    void OnEnable () {
        reached = false;
        timer = 0f;
        textObject = textObject.GetComponent<TextToUI>();
        
    }
 

	void Update ()
    {
        StayTimer();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "GollumWaypoint")
        {
            if (gameObject.tag == "Gollum")
            {
                reached = true;
            }
        }

        if (other.gameObject.tag == "SamWaypoint")
        {
            if (gameObject.tag == "Sam")
            {
                reached = true;
            }
        }
    }

    public void StayTimer()
    {
        if (reached)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                SendEvent("SaidSomething");
            }
        }
    }

}


