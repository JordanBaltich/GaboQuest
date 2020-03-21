using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibeeController : MonoBehaviour
{
    Animator m_StateMachine;
    BounceArc m_BounceArc;
    Rigidbody m_Body;

    public Vector3 bounceTarget;

    public int EnemyLayerID, GroundLayerID, PlayerLayerID;

    public bool bouncing;

    [SerializeField] float hitForce;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
        m_StateMachine = GetComponent<Animator>();
        m_BounceArc = GetComponent<BounceArc>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == GroundLayerID || collision.gameObject.layer == PlayerLayerID)
        {
            m_Body.isKinematic = false;
            ResetTriggers();

            if (collision.gameObject.layer == PlayerLayerID)
            {
                m_StateMachine.SetTrigger("isPickedUp");
            }
            if (collision.gameObject.layer == GroundLayerID)
            {
                m_StateMachine.SetTrigger("isIdle");
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == EnemyLayerID)
        {
            if (!bouncing)
            {
                Rigidbody enemyRB = other.gameObject.GetComponentInParent<Rigidbody>();
                if (other.gameObject.tag == "Weakpoint")
                {
                    other.gameObject.GetComponentInParent<Health>().TakeDamage(2);
                }
                else if (other.gameObject.tag == "HurtBox")
                {
                    other.gameObject.GetComponentInParent<Health>().TakeDamage(1);
                }

                bounceTarget = other.ClosestPointOnBounds(transform.position);
                ResetTriggers();
                m_StateMachine.SetTrigger("isBouncing");

                Knockback(enemyRB);

                if (other.gameObject.GetComponentInParent<Health>().currentHealth <= 0)
                {
                    other.gameObject.GetComponentInParent<Health>().DestroyObject();
                }

            }

        }
    }


    void Knockback(Rigidbody givenBody)
    {
        Vector3 direction = givenBody.transform.position - transform.position;

        givenBody.AddForce(direction.normalized * hitForce, ForceMode.Impulse);
    }

    public void ResetTriggers()
    {
        m_StateMachine.ResetTrigger("isDead");
        m_StateMachine.ResetTrigger("isIdle");
        m_StateMachine.ResetTrigger("isBouncing");
        m_StateMachine.ResetTrigger("isPickedUp");
        m_StateMachine.ResetTrigger("isShot");
    }
}
