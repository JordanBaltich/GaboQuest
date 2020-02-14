using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform destination;

    public Vector3 yDir;
    public Vector3 dir;
    public Vector3 distanceDir;
    [Range(1, 50)]
    public float ySpeed;
    [Range(1, 50)]
    public float firstSpeed;
    [Range(1, 50)]
    public float secondSpeed;
    [Range (1, 100)]
    public float chaseSpeed;
    [Range(-5, 5)]
    public float yPositionThreshold;
    [Range(1, 5)]
    public float firstPositionThreshold;
    [Range(1, 5)]
    public float secondPositionThreshold;

    public bool flipLookDir;
    public bool moving;
    public bool looking;


    private void LateUpdate()
    {
        if (destination != null)
        {
            Move(destination);
        }
    }

    //void FixedUpdate()
    //{
    //    if (destination != null)
    //    {
    //        Move(destination);
    //    }

    //}
    private void Move(Transform destination)
    {
        
        dir = Vector3.Normalize(destination.position - transform.position);

        Vector3 lerpDir = Vector3.Lerp(transform.position, dir, chaseSpeed);
        distanceDir = new Vector3(lerpDir.x, 0f, lerpDir.z);

        yDir = new Vector3(0f, destination.position.y - transform.position.y, 0f);

        if (yDir.y !=0)
        {
            transform.position += yDir * Time.smoothDeltaTime * ySpeed;
        }

        if (Vector3.Distance(transform.position, destination.position) >= firstPositionThreshold)
        {
            if (moving)
            {
                transform.position += distanceDir * Time.smoothDeltaTime * firstSpeed;
                //if (Vector3.Distance(transform.position, destination.position) >= secondPositionThreshold)
                //{
                //    transform.position += distanceDir * Time.smoothDeltaTime * secondSpeed;
                //}
            }

                if (looking)
            {
                if (flipLookDir)
                {
                    transform.rotation = Quaternion.LookRotation(-distanceDir);
                }
                else { transform.rotation = Quaternion.LookRotation(distanceDir); }

            }
        }

        
    }

}
