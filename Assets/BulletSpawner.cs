using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;

    public void SpawnBullet()
    {
        GameObject bulletInstance = Instantiate(BulletPrefab, this.transform);
        print(transform.name);
        bulletInstance.transform.parent = null;
    }
}
