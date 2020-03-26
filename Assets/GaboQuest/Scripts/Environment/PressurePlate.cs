using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] LerpTo LerpObject;

    [SerializeField] int layerID;


    [SerializeField] int weightLimit;

    PlayerController gaboController;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == layerID)
        {
            gaboController = other.gameObject.GetComponent<PlayerController>();

            if (gaboController.m_LibeeSorter.Normal.Count >= weightLimit)
            {
                LerpObject.ChangeState(0);
            }
        }
    }
}
