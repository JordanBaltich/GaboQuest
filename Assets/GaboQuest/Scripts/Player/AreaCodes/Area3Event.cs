using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Area3Event : MonoBehaviour
{
    public LevelTimeCheck timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Analytics.CustomEvent("Room3Reached", new Dictionary<string, object>
            {
                { "TookThisLongToReach", timer.timer }
            });
        }
    }
}
