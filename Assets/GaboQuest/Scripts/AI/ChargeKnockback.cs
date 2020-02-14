using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeKnockback : MonoBehaviour
{
    Transform parentObjectTransform;
    InvulnerabilityTimer targetInvulnerability;

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
            targetInvulnerability = other.gameObject.GetComponent<InvulnerabilityTimer>();
            if (targetInvulnerability.isVulnerable)
            {
                print("pushing back");

                Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
                
                Vector3 KnockbackDirection = (other.gameObject.transform.position - parentObjectTransform.position).normalized;

                targetRigidbody.AddForce(new Vector3(KnockbackDirection.x * pushbackForce, 0, KnockbackDirection.z * pushbackForce), ForceMode.Impulse);


                targetInvulnerability.StartInvulnerabilityTimer();
            }
        }
    }

}
