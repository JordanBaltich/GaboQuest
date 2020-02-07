using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowShrinkMechanic : MonoBehaviour
{

    public float maxSize;
    public float minSize;

    Vector3 maxScale;
    Vector3 minScale;
    public Vector3 currentScale;

    [SerializeField] float growAmount;

    public float growDuration;
    public float shrinkDuration;

    public bool isGrowing;

    float startTimer;
    public AnimationCurve animationCurve;

    // Use this for initialization
    void Start()
    {
        //// Create Vector3s for both the min and max sizes
        //maxScale = new Vector3(maxSize, maxSize, maxSize);
        //minScale = new Vector3(minSize, minSize, minSize);
        //// set the scale of the object to the minimum scale
        //transform.localScale = minScale;
        //// initially, the object should grow
        //isGrowing = true;

        ResetTimer();

        currentScale = transform.localScale;
    }

    void ResetTimer()
    {
        startTimer = Time.time;
    }

    public IEnumerator Grow(float currentLibees)
    {
        maxScale = Vector3.one + Vector3.one * (growAmount * currentLibees);
        float time = 0;

        while (time <= growDuration)
        {
            float percent = time / growDuration;
            time += Time.deltaTime;

            //// calculate the current scale of the square using the percent and animation curve
            Vector3 newScale = Vector3.LerpUnclamped(currentScale, maxScale, animationCurve.Evaluate(percent));
            transform.localScale = newScale;

            yield return null;
        }

        currentScale = transform.localScale;
        ResetTimer();
       
    }


    public IEnumerator Shrink(float currentLibees)
    {
        minScale = Vector3.one + Vector3.one * (growAmount * currentLibees);

        float time = 0;

        while (time <= shrinkDuration)
        {
            float percent = time / growDuration;
            time += Time.deltaTime;

            //// calculate the current scale of the square using the percent and animation curve
            Vector3 newScale = Vector3.LerpUnclamped(currentScale, minScale, animationCurve.Evaluate(percent));
            transform.localScale = newScale;

            yield return null;
        }

        currentScale = transform.localScale;
        ResetTimer();
    }
}
