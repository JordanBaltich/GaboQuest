using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField] List<int> vulnerableToLayers;


    [SerializeField] float velocityThreshold = 0.5f;

    private void OnCollisionEnter(Collision other)
    {
        
        print("collidsion");

        if (vulnerableToLayers.Contains(other.gameObject.layer))
        {
            Rigidbody collisionRigidBody;

            collisionRigidBody = other.gameObject.GetComponent<Rigidbody>();

            if(collisionRigidBody.velocity.magnitude >= velocityThreshold)
                Destroy(gameObject);
        }
        
    }
}
