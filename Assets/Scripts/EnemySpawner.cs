using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner spawner;

    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int maxActiveEnemies = 5;
    [SerializeField] private float checkInterval = 1f;
    [SerializeField] private float startDelay = 10f;
    [SerializeField] private int waveSize = 3;
    [SerializeField] private int waveIncreaseAmount = 2;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private int currentWaveIndex = 0;
    private int enemiesSpawned = 0;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(startDelay);

        StartCoroutine(Spawner());
        StartCoroutine(CheckActiveEnemies());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        int enemiesSpawnedInWave = 0;

        while (true)
        {
            yield return wait;
            if (activeEnemies.Count < maxActiveEnemies)
            {
                GameObject enemyToSpawn = GetRandomEnemyPrefab();
                Vector3 randomSpawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, randomSpawnPosition, Quaternion.identity);
                activeEnemies.Add(spawnedEnemy);
                enemiesSpawned++;

                enemiesSpawnedInWave++;
                if (enemiesSpawnedInWave >= waveSize)
                {
                    enemiesSpawnedInWave = 0;
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
        maxActiveEnemies += waveIncreaseAmount;
        waveSize += waveIncreaseAmount;
        currentWaveIndex++;
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
