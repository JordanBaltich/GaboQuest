using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightPlatform : MonoBehaviour
{
    public Transform origin, target;
    [SerializeField] AnimationCurve lerpCurve;
    [SerializeField] float duration;

    public PressurePlate linkedPlate;

    Vector3 OriginPos;

    public bool down;

    private void Start()
    {
        OriginPos = origin.position;
    }

    public States currentState = States.Wait;

    public enum States
    {
        On,
        Off,
        Wait,
    }

    public void ChangeState(int newState)
    {
        currentState = (States)newState;
    }

    private void FixedUpdate()
    {
        if (!down)
        {
            if (OriginPos.y + linkedPlate.weightOnPlate != target.position.y)
            {
                StopAllCoroutines();
                origin.position = transform.position;
                target.position = new Vector3(target.position.x, OriginPos.y + linkedPlate.weightOnPlate, target.position.z);
                ChangeState(0);
            }
        }
        else
        {
            if (OriginPos.y - linkedPlate.weightOnPlate != target.position.y)
            {
                StopAllCoroutines();
                origin.position = transform.position;
                target.position = new Vector3(target.position.x, OriginPos.y - linkedPlate.weightOnPlate, target.position.z);
                ChangeState(0);
            }
        }
       
        if (currentState == States.On)
        {
            StartCoroutine(LerpToPosition(origin, target));

            currentState = States.Wait;
        }
        else if (currentState == States.Off)
        {
            StartCoroutine(LerpToPosition(target, origin));

            currentState = States.Wait;
        }
    }

    IEnumerator LerpToPosition(Transform start, Transform end)
    {
        float time = 0;

        while (time <= duration)
        {
            float percent = time / duration;
            time += Time.fixedDeltaTime;
            transform.position = Vector3.LerpUnclamped(start.position, end.position, lerpCurve.Evaluate(percent));

            yield return null;
        }

        origin.position = transform.position;
    }
}
