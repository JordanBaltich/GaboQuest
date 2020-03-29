using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bopping_Motion : MonoBehaviour
{

    [SerializeField] float Frequency;
    [SerializeField] float Magnitude;

    Vector3 initial_Position;

    private void Start()
    {
        initial_Position.z = transform.position.z;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Mathf.Sin(Time.time * Frequency) * Magnitude;
    }
}
