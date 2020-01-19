using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public float acceleration;
    public float deceleration;

    public float Accelerate()
    {
        speed += acceleration * Time.deltaTime;

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        return speed;
    }

    public float Decelerate()
    {
        speed -= acceleration * Time.deltaTime;

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        return speed;
    }
}
