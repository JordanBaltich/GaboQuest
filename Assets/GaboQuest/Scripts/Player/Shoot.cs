using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private float shotForce;
    [SerializeField] Transform bulletSpawn;

    IEnumerator routine;

    float waitTime = 0;

    public void StartShooting(List<Transform> bullets)
    {
        if (routine == null)
        {
            routine = FireBullet(bullets);
            StartCoroutine(routine);
        }
    }

    public void StopShooting()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    IEnumerator FireBullet(List<Transform> bullets)
    {
        float timeBetweenShots = 1f / fireRate;

        while (bullets.Count > 0)
        {
            Rigidbody bulletBody = bullets[bullets.Count -1].GetComponent<Rigidbody>();
            bullets[bullets.Count - 1].transform.parent = null;
            bullets[bullets.Count - 1].transform.position = bulletSpawn.position;
            bullets[bullets.Count - 1].transform.rotation = bulletSpawn.rotation;

            bulletBody.useGravity = true;
            bulletBody.AddForce(bulletSpawn.transform.forward * shotForce, ForceMode.Impulse);
            bullets.Remove(bullets[bullets.Count - 1]);
          

            yield return new WaitForSeconds(timeBetweenShots);
            
        }

        //routine = null;
    }
}
