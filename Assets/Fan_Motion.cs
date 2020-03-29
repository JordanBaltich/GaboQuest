using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan_Motion : MonoBehaviour
{

    Transform m_transform;
    Vector3 rotationVector;
    [SerializeField] float rotationSpeed;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
    }
}
