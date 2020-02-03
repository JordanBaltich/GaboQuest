using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCannon : MonoBehaviour
{
    [SerializeField] GameObject LibeePrefab;

    [SerializeField] private float shotForce;

    public Transform shotOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SpawnLibee();
        }
    }

    void SpawnLibee()
    {
        GameObject currentLibee = Instantiate(LibeePrefab, shotOrigin.position, Quaternion.identity);

        currentLibee.GetComponent<Rigidbody>().AddForce(Vector3.forward * shotForce, ForceMode.Impulse);
    }
}
