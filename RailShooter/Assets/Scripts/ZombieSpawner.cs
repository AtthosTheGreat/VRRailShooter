using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public bool isSpawning = false;

    [SerializeField]
    private float spawnRadius = 8f;

    [SerializeField]
    private float spawnFrequency = 1.3f;

    [SerializeField]
    private GameObject zombiePrefab;

    private float lastSpawnTime = -100f;

    void Update()
    {
        if (isSpawning == false)
            return;

        if (Time.time - lastSpawnTime > spawnFrequency)
        {
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));
            Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

            lastSpawnTime = Time.time;
        }
    }
}
