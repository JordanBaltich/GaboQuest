using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] LerpTo LerpObject;

    [SerializeField] int layerID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerID)
        {
            if (LerpObject.name == "Bridge")
            {
                LerpObject.ChangeState(0);
            }
        }
    }
}
