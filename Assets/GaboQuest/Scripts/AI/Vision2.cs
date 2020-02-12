using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;
using System;

public class Vision2 : MonoBehaviour
{

    [SerializeField]
    List<GameObject> ObjectsInVolume;
    public List<string> targetList;
    public List<string> friendList;

    Blackboard blackboard;

    GameObjectVar currentTarget;
    public Color debugSightColor;
    public Color debugOccludedColor;

    private void Awake()
    {
        blackboard = GetComponentInParent<Blackboard>();
        currentTarget = blackboard.GetGameObjectVar("Target");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetList.Contains(other.tag))
            ObjectsInVolume.Add(other.gameObject);

    }

    private void Update()
    {
        for (int i = ObjectsInVolume.Count - 1; i >= 0; i--)
        {
            GameObject objectInVolume = ObjectsInVolume[i];

            if (objectInVolume == null || !objectInVolume.activeSelf)
            {
                ObjectsInVolume.Remove(objectInVolume);
            }

            Vector3 rayDirection = objectInVolume.transform.position - transform.parent.transform.position;
            Ray ray = new Ray(transform.parent.transform.position, rayDirection);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100);


            if (hit.collider.gameObject == objectInVolume)
            {
                Debug.DrawLine(transform.parent.transform.position, objectInVolume.transform.position, debugSightColor);
                currentTarget.Value = objectInVolume;

            }
            else if (hit.collider.gameObject != objectInVolume)
            {
                Debug.DrawLine(transform.parent.transform.position, objectInVolume.transform.position, debugOccludedColor);
                currentTarget.Value = null;
            }
        }
    }
}
