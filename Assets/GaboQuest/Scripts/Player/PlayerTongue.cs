using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTongue : MonoBehaviour
{
    public Transform tongueTarget;

   // [SerializeField] float distance;
    [SerializeField] float duration;
    [SerializeField] AnimationCurve SpeedCurve;

    LineRenderer m_LR;

    [SerializeField] int LibeeID;


    private void Awake()
    {
        m_LR = GetComponent<LineRenderer>();
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
            other.transform.parent = this.transform;
        }
    }


   
    public IEnumerator ShootTongue()
    {
        float time = 0;
        while (time <= duration)
        {
            float percent = time / duration;
            time += Time.deltaTime;
            transform.position = Vector3.LerpUnclamped(transform.position, tongueTarget.position, SpeedCurve.Evaluate(percent));

            yield return null;
        }

        StartCoroutine(RetractTongue());
    }

    public IEnumerator RetractTongue()
    {
        float time = 0;
        float retractDuration = duration / 2;
        while (time <= retractDuration)
        {
            float percent = time / retractDuration;
            time += Time.deltaTime;
            transform.position = Vector3.LerpUnclamped(transform.position, gameObject.transform.parent.transform.position, SpeedCurve.Evaluate(percent));

            yield return null;
        }

        GetComponentInParent<Animator>().SetBool("isTongueOut", false);
    }
}
