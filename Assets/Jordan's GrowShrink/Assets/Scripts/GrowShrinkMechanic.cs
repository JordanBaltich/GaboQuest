using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowShrinkMechanic : MonoBehaviour
{
    #region Size Variables
    public float maxSize;
    public float minSize;

    Vector3 maxScale;
    Vector3 minScale;
    #endregion

    #region Shrink Code

    public bool isShrinking;
    public bool isFullyShrunk = false;

    #endregion

    #region Grow Code


    public bool isGrowing;
    public bool isFullyGrown = false;
    #endregion

    float startTimer;
    public AnimationCurve animationCurve;
    public float sizeDuration;

    // Use this for initialization
    void Start()
    {
        // Create Vector3s for both the min and max sizes
        maxScale = new Vector3(maxSize, maxSize, maxSize);
        minScale = new Vector3(minSize, minSize, minSize);
        // set the scale of the object to the minimum scale
        transform.localScale = transform.localScale;
        // initially, the object should grow
        isShrinking = false;
        isGrowing = false;
        ResetTimer();

    }

    void ResetTimer()
    {
        startTimer = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        //Shrink using left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            isShrinking = true;
            minSize -= 0.1f;
            maxSize = minSize + 0.1f;
            minScale = new Vector3(minSize, minSize, minSize);
            isFullyShrunk = false;
            isFullyGrown = false;
            isGrowing = false;
        }

        //Grow using right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            isGrowing = true;
            maxSize += 0.1f;
            minSize = maxSize - 0.1f;
            maxScale = new Vector3(maxSize, maxSize, maxSize);
            isFullyGrown = false;
            isFullyShrunk = false;
            isShrinking = false;
        }

        //Activate Shrink code
        if (isShrinking == true)
        {
            Shrinker();

        }

        //Activate Grow code
        if (isGrowing == true)
        {
            Grower();

        }

    }

    public void Shrinker()
    {
        // calculate the time elapsed since the timer was started
        float timeElapsed = Time.time - startTimer;
        // get the percent of time elapsed in relation to the grow duration
        float percent = timeElapsed / sizeDuration;

        // if the timer has elapsed, switch states
        if (percent > 1)
        {
            transform.localScale = minScale;
            isShrinking = false;
            ResetTimer();
            return;
        }
        if (percent > 0.2)
        {
            isFullyShrunk = true;
        }

        if (percent < 0.2)
        {
            isFullyShrunk = false;
        }
        // calculate the current scale of the square using the percent and animation curve
        Vector3 newScale = Vector3.Lerp(maxScale, minScale, animationCurve.Evaluate(percent));
        transform.localScale = newScale;

    }

    public void Grower()
    {
        // calculate the time elapsed since the timer was started
        float timeElapsed = Time.time - startTimer;
        // get the percent of time elapsed in relation to the grow duration
        float percent = timeElapsed / sizeDuration;

        // if the timer has elapsed, switch states
        if (percent > 1)
        {
            transform.localScale = maxScale;
            isGrowing = false;
            ResetTimer();
            return;
        }
        if (percent > 0.2)
        {
            isFullyGrown = true;
        }

        if (percent < 0.2)
        {
            isFullyGrown = false;
        }
        // calculate the current scale of the square using the percent and animation curve
        Vector3 newScale = Vector3.Lerp(minScale, maxScale, animationCurve.Evaluate(percent));
        transform.localScale = newScale;

    }
}
