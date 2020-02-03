using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sam_Health : MonoBehaviour
{
    public bool healthy;
    [Range(1, 5)]
    public float health;
    // Update is called once per frame
    void Update()
    {
        if (health > 4f)
        {
            healthy = true;
        }
        else { healthy = false; }
    }



}
