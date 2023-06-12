using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner spawner;

    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int maxActiveEnemies = 5;
    [SerializeField] private float checkInterval = 1f;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private int currentWaveIndex = 0;
    private int enemiesSpawned = 0;

    private void Start()
    {
        StartCoroutine(Spawner());
        StartCoroutine(CheckActiveEnemies());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (true)
        {
            yield return wait;
            if (activeEnemies.Count < maxActiveEnemies)
            {
                GameObject enemyToSpawn = GetRandomEnemyPrefab();
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
                activeEnemies.Add(spawnedEnemy);
                enemiesSpawned++;

                if (enemiesSpawned >= maxActiveEnemies)
                {
                    enemiesSpawned = 0;
                    IncreaseDifficulty();
                }
            }
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    private void IncreaseDifficulty()
    {
        maxActiveEnemies++;
        currentWaveIndex++;
        checkInterval++;
    }

    private IEnumerator CheckActiveEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(checkInterval);

        while (true)
        {
            yield return wait;

            for (int i = activeEnemies.Count - 1; i >= 0; i--)
            {
                if (activeEnemies[i] == null)
                {
                    activeEnemies.RemoveAt(i);
                }
            }
        }
    }

    private void Awake()
    {
        spawner = this;
    }
}