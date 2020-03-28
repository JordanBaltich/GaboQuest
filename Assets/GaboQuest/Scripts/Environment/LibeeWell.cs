using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibeeWell : MonoBehaviour
{
    [SerializeField] Transform Player;

    [SerializeField] SortSelectLibee LibeeSorter;

    [SerializeField] float returnRadius;

    private void Awake()
    {
        if(Player == null)
            Player = GameObject.Find("Player").transform;

        if (LibeeSorter == null)
            LibeeSorter = GameObject.Find("PersistentGM").GetComponent<SortSelectLibee>();
    }

    void ReturnLibee()
    {

        foreach (Transform libee in LibeeSorter.Dead)
        {        
            libee.parent = null;
            libee.position = transform.position;
            libee.GetComponent<Rigidbody>().useGravity = false;
            libee.gameObject.GetComponent<LibeeController>().bounceTarget = transform.position;
            libee.gameObject.GetComponent<LibeeController>().ResetTriggers();
            libee.gameObject.GetComponent<Animator>().SetTrigger("isBouncing");

            LibeeSorter.Dead.Remove(libee);
            LibeeSorter.SortDeadLibees();
        }


    }

    private void FixedUpdate()
    {
        Vector3 direction = Player.position - transform.position;
        float distance = direction.magnitude;

        if (distance < returnRadius)
        {
            if (LibeeSorter.Dead.Count > 0)
            {
                ReturnLibee();
            }

        }
    }
}
