using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    
    public ProjectileProperties m_Projectile;

    float m_force;

    private void Awake()
    {
        FlyForward();
        m_force = m_Projectile.Force;
    }


    void FlyForward()
    {
        m_Rigidbody.AddForce(transform.forward * m_force, ForceMode.Impulse);
    }

}
