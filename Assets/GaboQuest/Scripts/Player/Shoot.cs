﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private AnimationCurve chargeCurve;

    [SerializeField] private float fireRate;
    [SerializeField] private float shotForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float minForce;
    [SerializeField] private float chargeTime;

    public Transform bulletSpawn;
    [SerializeField] AnimationCurve fallCurve;


    bool canFire = true;

    IEnumerator routine;
    IEnumerator chargeRoutine;

    float waitTime = 0;

    public void StartShotCooldown()
    {
        if (routine == null)
        {
            routine = FireRate();
            StartCoroutine(routine);
        }
    }

    public void StopShotCooldown()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    public void StartCharge(Rewired.Player p)
    {
        if (chargeRoutine == null)
        {
            chargeRoutine = BuildForce(p);
            StartCoroutine(chargeRoutine);
        }
    }

    public void StopCharge()
    {
        if (chargeRoutine != null)
        {
            StopCoroutine(chargeRoutine);
            chargeRoutine = null;
        }
    }

    public IEnumerator BuildForce(Rewired.Player p)
    {
        shotForce = minForce;
        float time = 0;
        while (time < chargeTime)
        {
            float percent = time / chargeTime;
            time += Time.deltaTime;
            shotForce = Mathf.LerpUnclamped(minForce, maxForce, chargeCurve.Evaluate(percent));
            yield return null;
        }
     
    }

    IEnumerator FireRate()
    {
        canFire = false;
        float timeBetweenShots = 1f / fireRate;
        yield return new WaitForSeconds(timeBetweenShots);
        canFire = true;
    }

    public void FireBullet(List<Transform> bullets)
    {
        if (canFire && bullets.Count > 0)
        {
            bullets[bullets.Count - 1].gameObject.GetComponent<LibeeController>().ResetTriggers();
            bullets[bullets.Count - 1].gameObject.GetComponent<Animator>().SetTrigger("isShot");
            bullets[bullets.Count - 1].transform.position = bulletSpawn.position;
            bullets[bullets.Count - 1].transform.rotation = bulletSpawn.rotation;
            bullets[bullets.Count - 1].transform.parent = null;

            bullets[bullets.Count - 1].gameObject.GetComponent<LibeeController>().shotDirection = GetComponent<PlayerController>().AimPoint.position - transform.position;

            Vector3 endPoint = GetComponent<PlayerController>().AimPoint.transform.position;
            bullets[bullets.Count - 1].GetComponent<LibeeController>().StartCoroutine(bullets[bullets.Count - 1].GetComponent<LibeeController>().BulletTravel(endPoint, shotForce, fallCurve, GetComponent<PlayerController>().AimPoint));

            bullets.Remove(bullets[bullets.Count - 1]);
            StartShotCooldown();
        }        
    }
}
