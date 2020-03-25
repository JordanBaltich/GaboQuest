using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    Rigidbody m_Body;

    [SerializeField]
    int playerLayerID;

    [SerializeField]
    float slowMultiplier;

    Rigidbody playerBody;

    float zDistance;
    float xDistance;

    Vector3 moveDirection;

    public int weight;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayerID)
        {
            playerBody = other.gameObject.GetComponent<Rigidbody>();

            zDistance = Mathf.Abs(playerBody.transform.position.z - transform.position.z);
            xDistance = Mathf.Abs(playerBody.transform.position.z - transform.position.z);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == playerLayerID)
        {
            playerBody.AddForce(-playerBody.velocity * slowMultiplier);

            m_Body.velocity = MoveDirection(xDistance, zDistance);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayerID)
        {
            m_Body.velocity = Vector3.zero;
            xDistance = 0;
            zDistance = 0;
            playerBody = null;
        }
    }

    Vector3 MoveDirection(float xDis, float zDis)
    {
        if (xDis > zDis)
        {
            return new Vector3(playerBody.velocity.x, m_Body.velocity.y, m_Body.velocity.z);
        }
        else if (xDis < zDis)
        {
            return new Vector3(m_Body.velocity.x, m_Body.velocity.y, playerBody.velocity.z);
        }
        else return Vector3.zero;

    }
}
