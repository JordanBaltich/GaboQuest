using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] Transform _destination;
    [Range(-10, 5)]
    [SerializeField] float _startChaseDist;
    [SerializeField] bool _moving;
    [SerializeField] float _time;
    [SerializeField] AnimationCurve lerpCurve;

    //LateUpdate allows moving after all phisics calculation
    private void LateUpdate()
    {
        if (_destination != null)
        {
            Move(_destination);
        }
    }

    //Moving to destination with lerp
    private void Move(Transform destination)
    {
        float percent = _time / Time.deltaTime;
        Vector3 lerpDir = Vector3.Lerp(transform.position, destination.position, lerpCurve.Evaluate(percent));

        if (Vector3.Distance(transform.position, destination.position) > 0.1f)
        {
            _time += Time.deltaTime;
            if (Vector3.Distance(transform.position, destination.position) >= _startChaseDist)
            {
                if (_moving)
                {
                    transform.position = lerpDir;
                    //transform.position = Vector3.MoveTowards(transform.position, destination.position, Time.deltaTime * 0.0001f);
                }

            }
             
        }
        else _time = 0;
    }

}
