using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] LerpTo LerpObject;

    [SerializeField] int layerID;

    int lastState = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerID)
        {
            if (LerpObject.name == "Bridge")
            {
                if (lastState == 1)
                {
                    LerpObject.ChangeState(0);
                    lastState = 0;
                }
                else
                {
                    LerpObject.ChangeState(1);
                    lastState = 1;
                }
              
            }

            

            if (LerpObject.name != "Bridge")
            {
                if(layerID == 11)
                {
                    LerpObject.ChangeState(0);
                }
                else
                {
                    if (lastState == 1)
                    {
                        LerpObject.ChangeState(0);
                        lastState = 0;
                    }
                    else
                    {
                        LerpObject.ChangeState(1);
                        lastState = 1;
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(LerpObject.name != "Bridge")
        {
            if (layerID == 11)
                LerpObject.ChangeState(1);
        }
    }
}
