using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] Material offMat, onMat;
    [SerializeField] MeshRenderer targetMesh;

    [SerializeField] int targetLayer, tongueLayer;

    [SerializeField]
    Transform LibeeStorage;
    public GameObject heldLibee;

    public States currentState = States.Off;

    public bool isGateLantern;

    public enum States
    {
        On,
        Off,
    }

    public void ChangeState(int newState)
    {
        currentState = (States)newState;
    }

    private void FixedUpdate()
    {
        if (currentState == States.On)
        {
            targetMesh.material = onMat;
        }
        else if (currentState == States.Off)
        {
            targetMesh.material = offMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == targetLayer)
        {
            if (currentState == States.Off)
            {
                if (heldLibee == null)
                {
                    print("GotLibee");
                    heldLibee = other.gameObject;
                    currentState = States.On;

                    heldLibee.transform.parent = LibeeStorage.transform;

                    heldLibee.GetComponent<LibeeController>().StopAllCoroutines();

                    Rigidbody libBody = heldLibee.GetComponent<Rigidbody>();
                    libBody.useGravity = false;
                    libBody.isKinematic = true;
                    libBody.velocity = Vector3.zero;


                    libBody.transform.position = LibeeStorage.position;
                }


            }
            else
            {
                currentState = States.Off;
            }
        }

        if (other.gameObject.layer == tongueLayer)
        {
            if (currentState == States.On)
            {
                if (heldLibee != null)
                {
                    heldLibee.GetComponent<LibeeController>().ResetTriggers();
                    heldLibee.GetComponent<Animator>().SetTrigger("isIdle");
                    heldLibee.transform.parent = other.gameObject.transform;
                    heldLibee.transform.position = other.transform.position;

                    currentState = States.Off;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == tongueLayer)
        {
            if (heldLibee != null)
                heldLibee = null;
        }
    }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.layer == targetLayer)
        //    {
        //        if (currentState == States.Off)
        //        {

        //            heldLibee = collision.gameObject;
        //            currentState = States.On;



        //        }
        //        else
        //        {
        //            currentState = States.Off;
        //        }
        //    }

        //    if (collision.gameObject.layer == tongueLayer)
        //    {
        //        if (currentState == States.On)
        //        {
        //            if (LibeeStorage.GetComponentInChildren<Transform>() != null)
        //            {
        //                collision.gameObject.GetComponent<Animator>().SetTrigger("isIdle");
        //                collision.gameObject.transform.parent = collision.gameObject.transform;
        //                collision.gameObject.transform.position = Vector3.zero;

        //                currentState = States.Off;
        //            }
        //        }
        //    }
        //}

    }
