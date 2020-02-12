using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeKnockback : MonoBehaviour
{
    Transform parentObjectTransform;

    public float pushbackForce;

    // Start is called before the first frame update
    void Start()
    {
        parentObjectTransform = GetComponentInParent<Transform>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Libee")
        {

            Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

            Vector3 KnockbackDirection = (other.gameObject.transform.position - parentObjectTransform.position).normalized;

            targetRigidbody.AddForce(KnockbackDirection * pushbackForce, ForceMode.Impulse);
        }
    }

}
