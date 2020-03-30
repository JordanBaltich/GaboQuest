using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTo : MonoBehaviour
{
    public Transform origin, target;
    [SerializeField] AnimationCurve lerpCurve;
    [SerializeField] float duration;

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
        if (currentState == States.On)
        {
            StartCoroutine(LerpToPosition(origin, target));
            this.enabled = false;
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
            transform.position = Vector3.Lerp(start.position, end.position, lerpCurve.Evaluate(percent));

            yield return null;
        }
    }
}
