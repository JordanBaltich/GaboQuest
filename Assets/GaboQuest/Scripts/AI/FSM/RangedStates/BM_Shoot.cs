using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class BM_Shoot : StateBehaviour
{
    GameObject obj_bullet;
    [SerializeField] float TimeBeforeIdle;
    [SerializeField] BulletSpawner m_BulletSpawner;

    void OnEnable()
    {
        Debug.Log("Started *Shoot*");
        if(m_BulletSpawner == null)
            m_BulletSpawner = GetComponentInChildren<BulletSpawner>();
        
        StartCoroutine(ShootTarget());
    }

    // Called when the state is disabled
    void OnDisable()
    {
        Debug.Log("Stopped *Shoot*");
        StopAllCoroutines();
    }

    
    IEnumerator ShootTarget()
    {
        m_BulletSpawner.SpawnBullet();
        yield return new WaitForSeconds(TimeBeforeIdle);
        SendEvent("Idling");

    }
}
