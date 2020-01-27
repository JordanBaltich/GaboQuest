using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform destination;

    public Vector3 dir;
    public Vector3 distanceDir;
    [Range(1, 50)]
    public float firstSpeed;
    [Range(1, 50)]
    public float secondSpeed;
    [Range (1, 100)]
    public float chaseSpeed;
    [Range(1, 5)]
    public float firstPositionThreshold;
    [Range(1, 5)]
    public float secondPositionThreshold;

    public bool flipLookDir;
    public bool moving;
    public bool looking;
    void FixedUpdate()
    {
        if (destination != null)
        {
            Move(destination);
        }

    }
    private void Move(Transform destination)
    { 
        dir = Vector3.Normalize(destination.position - transform.position);
        Vector3 lerpDir = Vector3.Lerp(transform.position, dir, chaseSpeed);
        distanceDir = new Vector3(lerpDir.x, 0f, lerpDir.z);

        if (Vector3.Distance(transform.position, destination.position) >= firstPositionThreshold)
        {
            if (moving)
            {
                transform.position += distanceDir * Time.smoothDeltaTime * firstSpeed;
                if (Vector3.Distance(transform.position, destination.position) >= firstPositionThreshold)
                {
                    transform.position += distanceDir * Time.smoothDeltaTime * secondSpeed;

                }
            }

                if (looking)
            {
                if (flipLookDir)
                {
                    transform.rotation = Quaternion.LookRotation(-lerpDir);
                }
                else { transform.rotation = Quaternion.LookRotation(lerpDir); }

            }
        }
    }

}
