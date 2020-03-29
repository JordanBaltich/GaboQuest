using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlatforms : MonoBehaviour
{
    public int[] targetLayers;

    float distanceBetween;

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < targetLayers.Length; i++)
        {
            if (collision.gameObject.layer == targetLayers[i])
            {
                collision.gameObject.transform.parent = transform;
                //distanceBetween = Vector3.Distance(transform.position, collision.gameObject.transform.position);
            }
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    for (int i = 0; i < targetLayers.Length; i++)
    //    {
    //        if (collision.gameObject.layer == targetLayers[i])
    //        {
    //          float currentDistance = Vector3.Distance(transform.position, collision.gameObject.transform.position);
    //            if (currentDistance != distanceBetween)
    //            {
    //                collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, transform.position.y, collision.gameObject.transform.position.z) + new Vector3(0, distanceBetween, 0);
    //            }
    //        }
    //    }
    //}

    private void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < targetLayers.Length; i++)
        {
            if (collision.gameObject.layer == targetLayers[i])
            {
                if(collision.gameObject.transform.parent == transform)
                collision.gameObject.transform.parent = null;
            }
        }
    }
}
