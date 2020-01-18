using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_Body;

    PlayerMotor m_Motor;

    Vector3 direction;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();

        m_Motor = GetComponent<PlayerMotor>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(Direction().x, 0, Direction().y);

        if (Direction().sqrMagnitude > 0.15f || Direction().sqrMagnitude < - 0.15f)
        {
            m_Body.velocity = moveDirection * m_Motor.Accelerate();
        }
        else
        {
            m_Motor.Decelerate();

            if (m_Body.velocity.sqrMagnitude > 0.15f)
            {
                m_Body.velocity = moveDirection * m_Motor.Decelerate();
            }
            
        }
       
    }

    private void FixedUpdate()
    {
        
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
