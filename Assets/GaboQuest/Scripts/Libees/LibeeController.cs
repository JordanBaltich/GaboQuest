using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LibeeController : MonoBehaviour
{
    BounceArc m_BounceArc;
    Rigidbody m_Body;

    public int EnemyLayerID, GroundLayerID, PlayerLayerID;

    [SerializeField] float hitForce;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();

        m_BounceArc = GetComponent<BounceArc>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       

        if (collision.gameObject.layer == GroundLayerID || collision.gameObject.layer == PlayerLayerID)
        {
            m_Body.isKinematic = false;
            m_BounceArc.currentPosition = 0;
            Destroy(m_BounceArc.currentLandingTarget);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == EnemyLayerID)
        {
            Rigidbody enemyRB = other.gameObject.GetComponentInParent<Rigidbody>();
            if (other.gameObject.tag == "Weakpoint")
            {
                m_BounceArc.BounceOffTarget(other.transform);
                other.gameObject.GetComponentInParent<Health>().TakeDamage(2);
                Knockback(enemyRB);
                Analytics.CustomEvent("LibeeHitWeakpoint");
            }
            else if (other.gameObject.tag == "HitBox")
            {
                m_BounceArc.BounceOffTarget(other.transform);
                other.gameObject.GetComponentInParent<Health>().TakeDamage(1);
                Knockback(enemyRB);
                Analytics.CustomEvent("LibeeHitEnemyButNotWeakpoint");
            }

            if (other.gameObject.GetComponentInParent<Health>().currentHealth <= 0)
            {
                other.gameObject.GetComponentInParent<Health>().DestroyObject();
                Analytics.CustomEvent("LibeeKilledEnemy");
            }
        }
    }


    void Knockback(Rigidbody givenBody)
    {
        Vector3 direction = givenBody.transform.position - transform.position;

        givenBody.AddForce(direction.normalized * hitForce, ForceMode.Impulse);
    }
}
