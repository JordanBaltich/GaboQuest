using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class BM_Shoot : StateBehaviour
{
    GameObject obj_bullet;

    void OnEnable()
    {
        Debug.Log("Started *Shoot*");
    }

    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Stopped *Shoot*");
        StopAllCoroutines();

    }

    
    void ShootTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position);
        direction = direction.normalized;

        //transform.rotation = Quaternion.Lerp(transform.rotation, direction, 1f);
    }

    void SpawnBullet()
    {
        Instantiate(obj_bullet);
    }
}
