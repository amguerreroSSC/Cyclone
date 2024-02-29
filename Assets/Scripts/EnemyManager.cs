using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float initialSpawnRate;
    [SerializeField] float spawnAreaWidth;
    [SerializeField] float spawnAreaHeight;

    float spawnRate;


    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1.0f, initialSpawnRate);
    }

    private void Initialize()
    {
        spawnRate = initialSpawnRate;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, RandomPos(), Quaternion.identity);
    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-spawnAreaHeight, spawnAreaWidth), Random.Range(-spawnAreaHeight, spawnAreaHeight));
    }
}
