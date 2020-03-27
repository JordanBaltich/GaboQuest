using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlatforms : MonoBehaviour
{
    public int[] targetLayers;

    Transform currentParent;

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < targetLayers.Length; i++)
        {
            if (collision.gameObject.layer == targetLayers[i])
            {
              
               
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < targetLayers.Length; i++)
        {
            if (collision.gameObject.layer == targetLayers[i])
            {
              
            }
        }
    }
}
