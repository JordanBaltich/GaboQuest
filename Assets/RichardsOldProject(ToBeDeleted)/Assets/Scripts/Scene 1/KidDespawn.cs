using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidDespawn : MonoBehaviour
{
    bool hit;
    private void OnEnable()
    {
        hit = false;
    }
    private void Update()
    {
        if (hit)
        {
            transform.position = new Vector3(10000000f, 1000000f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            hit = true;
        }
    }
}
