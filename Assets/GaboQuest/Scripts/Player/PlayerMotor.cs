using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public float acceleration;
    public float deceleration;
    public float maxWithWeight;
    public float currentSpeed;

    public float Accelerate()
    {
        speed += acceleration * Time.deltaTime;

        speed = Mathf.Clamp(speed, 0, maxWithWeight);

        return speed;
    }

    public float Decelerate()
    {
        speed -= deceleration * Time.deltaTime;

        speed = Mathf.Clamp(speed, 0, maxWithWeight);

        return speed;
    }

    public float MaxSpeedValue(float LibeeCount)
    {
        if (LibeeCount > 0)
        {
            maxWithWeight = maxSpeed - Mathf.Pow(LibeeCount, 0.5f);

            return maxWithWeight;
        }
        else
        {
            maxWithWeight = maxSpeed;
            return maxWithWeight;
        }
      
    }
}
