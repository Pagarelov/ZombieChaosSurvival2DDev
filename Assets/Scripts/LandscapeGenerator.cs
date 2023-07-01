using System.Collections.Generic;
using UnityEngine;

public class LandscapeGenerator : MonoBehaviour
{
    public GameObject[] landscapePrefabs; // Массив префабов элементов ландшафта
    public GameObject player; // Ссылка на объект игрока
    public float chunkSize = 10f; // Размер каждого чанка ландшафта
    public float radius = 20f; // Радиус области генерации ландшафта
    public int maxChunks = 5; // Максимальное количество предыдущих чанков, которые будут храниться

    private List<GameObject> chunks = new List<GameObject>(); // Список для хранения предыдущих чанков

    private void Start()
    {
        GenerateChunks();
    }

    private void Update()
    {
        // Проверяем, если игрок вышел за пределы текущего чанка
        if (!IsPlayerInCurrentChunk())
        {
            GenerateChunks();
            DestroyChunks();
        }
    }

    private bool IsPlayerInCurrentChunk()
    {
        // Проверяем, существует ли объект player
        if (player == null)
            return false;

        // Определяем текущий чанк, в котором находится игрок
        Vector3 playerPosition = player.transform.position;

        if (chunks.Count > 0)
        {
            Vector3 currentChunkPosition = chunks[chunks.Count - 1].transform.position;

            // Проверяем расстояние между игроком и текущим чанком
            float distanceToCurrentChunk = Vector3.Distance(playerPosition, currentChunkPosition);

            return distanceToCurrentChunk <= radius;
        }

        return false;
    }

    private void GenerateChunks()
    {
        // Определяем количество чанков, которые нужно сгенерировать
        int chunksToGenerate = Mathf.CeilToInt(radius / chunkSize);

        // Определяем позицию центрального чанка
        Vector3 centerPosition = player.transform.position;

        // Генерируем чанки вокруг игрока
        for (int i = 0; i < chunksToGenerate; i++)
        {
            // Вычисляем позицию для каждого чанка
            Vector3 chunkPosition = centerPosition + (Vector3.right * i * chunkSize) + (Vector3.right * chunkSize * 0.5f);

            // Случайным образом выбираем префаб элемента ландшафта
            GameObject randomPrefab = landscapePrefabs[Random.Range(0, landscapePrefabs.Length)];

            // Создаем новый чанк ландшафта из выбранного префаба
            GameObject newChunk = Instantiate(randomPrefab, chunkPosition, Quaternion.identity);

            // Добавляем новый чанк в список предыдущих чанков
            chunks.Add(newChunk);
        }
    }

    private void DestroyChunks()
    {
        // Проверяем, если количество предыдущих чанков превышает максимальное значение
        while (chunks.Count > maxChunks)
        {
            GameObject chunkToRemove = chunks[0];
            chunks.RemoveAt(0);
            Destroy(chunkToRemove);
        }
    }
}
