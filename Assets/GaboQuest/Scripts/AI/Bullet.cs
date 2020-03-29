using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    
    public ProjectileProperties m_Projectile;

    public float m_ImpulseForce;

    [SerializeField] List<int> DamageLayerIDs;

    [Range (1,3)]
    [SerializeField] int Damage_Dealt = 1;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        if (m_Projectile != null)
            m_ImpulseForce = m_Projectile.Force;

        FlyForward();
    }


    void FlyForward()
    {
        m_Rigidbody.AddForce(transform.forward * m_ImpulseForce, ForceMode.Impulse);
        //m_Rigidbody
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (DamageLayerIDs.Contains(collisionObject.layer))
        {
            Health m_Health = collisionObject.GetComponent<Health>();

            m_Health.currentHealth -= Damage_Dealt;
        }

        Destroy(gameObject);
    }

}
