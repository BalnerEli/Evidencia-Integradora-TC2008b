using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private float nextSpawn = 0f;

    // Controla la aparición de enemies
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnEnemy();
        }
    }

    // Genera un enemigo en una posición aleatoria en cierta area
    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
