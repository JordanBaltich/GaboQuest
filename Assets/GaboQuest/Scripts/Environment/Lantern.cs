using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] Material offMat, onMat;
    [SerializeField] MeshRenderer targetMesh;

    [SerializeField] int targetLayer;

    public States currentState = States.Off;

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
                currentState = States.On;
            }
            else
            {
                currentState = States.Off;
            }
        }
    }
}
