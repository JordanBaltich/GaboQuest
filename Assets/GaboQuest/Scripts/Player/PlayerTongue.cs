using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
public class PlayerTongue : MonoBehaviour
{
    public Transform tongueTarget;
    // [SerializeField] float distance;
    [SerializeField] float duration;
    [SerializeField] AnimationCurve SpeedCurve;
    LineRenderer m_LR;
    [SerializeField] int LibeeID;

    /// <summary>
    /// Aim Assist for Tongue
    /// </summary>
    //R* Set new Duration to >0.1f to cut off animation
    [SerializeField] float newDuration;

    //R* TongueBox is the object that stores the gameObject List
    [SerializeField] BoxTargets tongueBox;

    //R* Grab Target is the Tongue's Destination
    public Transform GrabTarget;

    //R* Grabbed Stops Destination from updating in Shoot State
    [SerializeField] bool Grabbed;


    private void Awake()
    {
        m_LR = GetComponent<LineRenderer>();

        //R* Setting the variable to avoid errors
        GrabTarget = tongueTarget;
        newDuration = duration;
    }

    public void RenderTongue()
    {
        m_LR.SetPosition(0, gameObject.transform.parent.transform.position);
        m_LR.SetPosition(1, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LibeeID)
        {
            Rigidbody libeeBody = other.gameObject.GetComponent<Rigidbody>();

            //R* Setting the libee to float state 
            // Needs fixing!
            libeeBody.useGravity = false;
            libeeBody.velocity = Vector3.zero;
            other.transform.parent = this.transform;

            //R* Stops tongueBox from updating 
            tongueBox.stopSorting = true;
            tongueBox.TargetsByRange.Clear();
            //R* Stop movement by setting destination to itself
            GrabTarget = transform;
            Analytics.CustomEvent("UsedTongueToGrabLibee");
        }
    }



    public IEnumerator ShootTongue()
    {
        //R* Stop destination from updating
        if (!Grabbed)
        {
            if (tongueBox == null)
            {
                print("tongueBox not assigned");
            }
            GrabTarget = tongueBox.ClosestTarget(tongueTarget);
            Grabbed = true;
        }

        float time = 0;

        while (time <= newDuration)
        {
            float percent = time / newDuration;
            time += Time.deltaTime;

            //#Original//
            //transform.position = Vector3.LerpUnclamped(transform.position, tongueTarget.position, SpeedCurve.Evaluate(percent));

            //R* Changed destination to GrabTarget
            transform.position = Vector3.LerpUnclamped(transform.position, GrabTarget.position, SpeedCurve.Evaluate(percent));

            yield return null;
        }

        StartCoroutine(RetractTongue());
    }

    public IEnumerator RetractTongue()
    {
        //R* Resetting Animation Time to default
        newDuration = duration;

        float time = 0;
        float retractDuration = newDuration / 2;
        while (time <= retractDuration)
        {
            float percent = time / retractDuration;
            time += Time.deltaTime;
            transform.position = Vector3.LerpUnclamped(transform.position, gameObject.transform.parent.transform.position, SpeedCurve.Evaluate(percent));

            yield return null;
        }

        //R* Resetting parameters to default
        Grabbed = false;
        tongueBox.stopSorting = false;
        GrabTarget = tongueTarget;

        GetComponentInParent<Animator>().SetBool("isTongueOut", false);
    }
}