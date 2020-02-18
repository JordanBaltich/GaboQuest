using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibeeWell : MonoBehaviour
{
    [SerializeField] Transform Player;

    [SerializeField] SortSelectLibee LibeeSorter;

    [SerializeField] float returnRadius;

    void ReturnLibee()
    {
        if (LibeeSorter.Dead.Count > 0)
        {
            foreach (Transform libee in LibeeSorter.Dead)
            {
                libee.parent = null;
                libee.position = transform.position;
                libee.GetComponent<Rigidbody>().useGravity = false;
                libee.gameObject.GetComponent<BounceArc>().BounceOffTarget(transform);
                LibeeSorter.Dead.Remove(libee);
            }
        }
       
    }

    private void FixedUpdate()
    {
        Vector3 direction = Player.position - transform.position;
        float distance = direction.magnitude;

        if (distance < returnRadius)
        {
            ReturnLibee();
        }
    }
}
