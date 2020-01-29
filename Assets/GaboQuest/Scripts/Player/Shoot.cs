using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private float shotForce;
    [SerializeField] Transform bulletSpawn;

    IEnumerator routine;
    
    public void StartShooting(GameObject bulletPrefab)
    {
        if (routine == null)
        {
            routine = FireBullet(bulletPrefab);
            StartCoroutine(routine);
        }
    }


    IEnumerator FireBullet(GameObject bulletPrefab)
    {
        float timeBetweenShots = 1f / fireRate;

        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward, bulletSpawn.rotation);
        Rigidbody bulletBody = newBullet.GetComponent<Rigidbody>();

        bulletBody.AddForce(newBullet.transform.forward * shotForce, ForceMode.Impulse);

        yield return new WaitForSeconds(timeBetweenShots);

        routine = null;
    }
}
