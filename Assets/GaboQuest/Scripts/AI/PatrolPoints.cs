using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolPoints : Agent
{
    public List<GameObject> waypoints;

    public int waypointIndex = 0;

    public void WalkToWaypoint()
    {
        //print("Set destination: " + waypoints[waypointIndex].transform.position);
        m_navAgent.SetDestination(waypoints[waypointIndex].transform.position);
    }

}
