using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocations : MonoBehaviour
{
    public GameObject Player;
    public GameObject Crosshair;
    public List<GameObject> Enemies = new List<GameObject>();
    public GameObject Closest;

    void Start()
    {
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    public Transform FindClosestEnemy()
    {
        for (int i = Enemies.Count - 1; i >= 0; i--)
        {
            if (Enemies[i] == null)
            {
                Enemies.Remove(Enemies[i]);
            }
        }
        Enemies.Sort((x, y) => {
            return
        
        Vector3.Distance(transform.TransformPoint(Crosshair.transform.position),
            transform.TransformPoint(x.transform.position)).
            CompareTo(Vector3.Distance(transform.TransformPoint(Crosshair.transform.position),
            transform.TransformPoint(y.transform.position))); }
        )
        ;

        Closest = Enemies[0];
        return Enemies[0].transform;
    }
}
