using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_Body;

    PlayerMotor m_Motor;
    Health m_Health;
    Vector3 direction;

    internal bool CanMove = true;

    [SerializeField] private int hazardLayerID, healthID;

    public float rotationSpeed;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();

        m_Motor = GetComponent<PlayerMotor>();
        m_Health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        //convert direction from Vector2 to Vector3
        Vector3 moveDirection = new Vector3(Direction().x, 0, Direction().y);

        
            //accelerate when input direction is past given threshold, rotate towards input direction
        if (Direction().sqrMagnitude > 0.15f || Direction().sqrMagnitude < -0.15f)
        {
            m_Body.velocity = moveDirection * m_Motor.Accelerate();

            m_Body.rotation = Quaternion.LookRotation((moveDirection * rotationSpeed * Time.deltaTime));
        }
        else
        {
            // decelerate when no input is given
            m_Motor.Decelerate();
            if (m_Body.velocity.sqrMagnitude > 0.15f)
            {
                m_Body.velocity = moveDirection * m_Motor.Decelerate();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == healthID)
        {
            //m_Health.Heal(1);
            //Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == hazardLayerID)
        {
            m_Health.TakeDamage(1);
            m_Body.AddForce(Vector3.up * 3, ForceMode.Impulse);
        }
    }

    Vector2 Direction()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(hor, vert);

        if (direction.magnitude > 1)
        {
            direction = direction / direction.magnitude;
        }

        return direction;
    }
}
