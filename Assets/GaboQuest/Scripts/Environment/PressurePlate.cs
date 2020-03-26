using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] LerpTo LerpObject;

    [SerializeField] int playerID, libeeID, boxID;


    [SerializeField] int weightLimit;

    PlayerController gaboController;

    public int weightOnPlate = 0;
    public int libeesOnPlate = 0;

    public bool forRisingPlat;

    int stepOnWeight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == libeeID)
        {
            libeesOnPlate++;
            //weightLimit++;
            weightOnPlate++;
        }

        if (other.gameObject.layer == playerID)
        {
            gaboController = other.gameObject.GetComponent<PlayerController>();
            stepOnWeight = gaboController.weight + gaboController.m_LibeeSorter.TotalLibees();

            weightOnPlate += stepOnWeight;

        }

        if (other.gameObject.layer == boxID)
        {
            weightOnPlate += other.gameObject.GetComponent<PushBox>().weight;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        

        if (other.gameObject.layer == playerID)
        {
            if (gaboController.weight + gaboController.m_LibeeSorter.Normal.Count != stepOnWeight)
            {
                weightOnPlate -= stepOnWeight;
                //stepOnWeight = gaboController.weight + gaboController.m_LibeeSorter.Normal.Count;
                stepOnWeight = gaboController.weight + gaboController.m_LibeeSorter.TotalLibees();
                weightOnPlate += stepOnWeight;

            }
        }

        if (forRisingPlat)
        {
            if (weightOnPlate > weightLimit)
            {
                weightOnPlate = weightLimit;
            }
        }

        if (weightOnPlate >= weightLimit)
        {
            if (LerpObject != null)
                LerpObject.ChangeState(0);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerID)
        {
            weightOnPlate -= gaboController.weight + gaboController.m_LibeeSorter.Normal.Count;

        }
        if (other.gameObject.layer == boxID)
        {
            weightOnPlate -= other.gameObject.GetComponent<PushBox>().weight;
        }

        if (other.gameObject.layer == libeeID)
        {
            libeesOnPlate--;
            weightOnPlate--;
        }

        if (forRisingPlat)
        {
            if (LerpObject != null)
            {
                LerpObject.target.position = new Vector3(LerpObject.target.position.x, LerpObject.origin.position.y + weightOnPlate, LerpObject.target.position.z);
                LerpObject.ChangeState(0);
            }
        }
    }
}
