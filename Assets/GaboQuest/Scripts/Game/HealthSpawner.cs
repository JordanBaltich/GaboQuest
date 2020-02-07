using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField] private GameObject healthPickupPrefab;

    [SerializeField] private float respawnTime;
    [SerializeField] private float heightOffset;

    [SerializeField] private GameObject heldItem;

    IEnumerator respawnRoutine;

    private void Awake()
    {
        SpawnHealthPickup();
    }

   
    // Update is called once per frame
    void Update()
    {
        if (heldItem == null)
        {
            if (respawnRoutine == null)
            {
                respawnRoutine = RespawnTimer();
                StartCoroutine(respawnRoutine);
            }
        }
    }

    void SpawnHealthPickup()
    {
        heldItem = Instantiate(healthPickupPrefab, transform.position + (Vector3.up * heightOffset), Quaternion.identity);
    }

    IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(respawnTime);

        SpawnHealthPickup();

        respawnRoutine = null;
    }
}
