using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool rawFood;
    public bool food;
    public bool artifact;

    public GameObject Artifact;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RawFood")
        {
            other.gameObject.SetActive(false);
            rawFood = true;
        }
        if (other.gameObject.tag == "Food")
        {
            other.gameObject.SetActive(false);
            food = true;
        }
        if (other.gameObject.tag == "Artifact")
        {
            other.gameObject.SetActive(false);
            artifact = true;
        }
        if (other.gameObject.tag == "ArtifactPoint")
        {
            if (artifact)
            {
                other.gameObject.SetActive(false);
                Artifact.SetActive(true);
            }
        }
    }
}
