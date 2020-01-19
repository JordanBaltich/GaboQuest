using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Peon : MonoBehaviour {

    public float wanderRadius;
    public Vector3 destination;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    UnityEngine.AI.NavMeshPath navMeshPath;

    public float arrivalDistance;

    void Awake() {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshPath = new UnityEngine.AI.NavMeshPath();
    }
    // Use this for initialization
    void Start() {
        SelectRandomDestination();
    }

    // Update is called once per frame
    void Update() {
        if (navMeshAgent.remainingDistance <= arrivalDistance) {
            SelectRandomDestination();
        }

        navMeshAgent.CalculatePath(destination, navMeshPath);
        navMeshAgent.SetPath(navMeshPath);
    }


    void SelectRandomDestination() {

        Vector3 randomPosition = Quaternion.Euler(new Vector3(90, 0, 0)) * Random.insideUnitCircle * wanderRadius;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomPosition, out hit, wanderRadius, UnityEngine.AI.NavMesh.AllAreas);
        destination = hit.position;

    }

}
