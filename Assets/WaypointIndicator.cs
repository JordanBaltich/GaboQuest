using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointIndicator : MonoBehaviour
{
    [Range(0,1)]
    public float GizmoSize = .2f;

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, GizmoSize);
    }
}
