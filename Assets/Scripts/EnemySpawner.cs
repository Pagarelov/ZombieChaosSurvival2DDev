using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner spawner; // Ссылка на себя для доступа из других скриптов

    [SerializeField] private float spawnRate = 1f; // Частота появления врагов
    [SerializeField] private float spawnRadius = 5f; // Радиус спавна врагов
    [SerializeField] private GameObject[] enemyPrefabs; // Массив префабов врагов
    [SerializeField] private int maxActiveEnemies = 5; // Максимальное количество активных врагов
    [SerializeField] private float checkInterval = 1f; // Интервал проверки активных врагов
    [SerializeField] private float startDelay = 10f; // Задержка перед началом спавна
    [SerializeField] private int waveSize = 3; // Размер волны
    [SerializeField] private int waveIncreaseAmount = 2; // Увеличение сложности между волнами

    private List<GameObject> activeEnemies = new List<GameObject>(); // Список активных врагов

    private int currentWaveIndex = 0; // Индекс текущей волны
    private int enemiesSpawned = 0; // Количество появившихся врагов

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
                Vector3 randomSpawnPosition = GetRandomSpawnPosition();
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

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPoint = Random.insideUnitSphere * spawnRadius;
        randomPoint += transform.position;
        randomPoint.y = transform.position.y; // Предполагается, что враги должны появляться на той же высоте, что и точка спавна
        return randomPoint;
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    private void IncreaseDifficulty()
    {
        maxActiveEnemies += waveIncreaseAmount; // Увеличиваем максимальное количество активных врагов
        waveSize += waveIncreaseAmount; // Увеличиваем размер волны
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
                if (activeEnemies[i] == null) // Если враг был уничтожен
                {
                    activeEnemies.RemoveAt(i); // Удаляем его из списка активных врагов
                }
            }
        }
    }

    private void Awake()
    {
        spawner = this; // Присваиваем ссылку на себя статической переменной для доступа из других скриптов
    }
}
