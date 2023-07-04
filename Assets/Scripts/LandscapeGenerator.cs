using UnityEngine;

public class LandscapeGenerator : MonoBehaviour
{
    [SerializeField] private Transform prefab; // Префаб для генерации ландшафта
    [SerializeField] private int _xSize; // Размер ландшафта по оси X
    [SerializeField] private int _ySize; // Размер ландшафта по оси Y
    [SerializeField] private int _x; // Количество объектов по оси X
    [SerializeField] private int _y; // Количество объектов по оси Y
    [SerializeField] private Transform _playerTransform; // Ссылка на трансформ игрока

    private bool _isLandscapeGenerated; // Флаг, указывающий, был ли ландшафт сгенерирован ранее
    private int _previousX; // Предыдущее значение _x
    private int _previousY; // Предыдущее значение _y
    private Vector3 _playerStartPosition; // Начальная позиция игрока

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока по тегу "Player" и получаем его трансформ
        _playerStartPosition = _playerTransform.position; // Сохраняем начальную позицию игрока
        GenerateLandscape(); // Генерируем ландшафт
    }

    private void GenerateLandscape()
    {
        if (_isLandscapeGenerated)
        {
            return; // Если ландшафт уже сгенерирован, прекращаем выполнение метода
        }

        for (int x = 0; x < _x; x++)
        {
            for (int y = 0; y < _y; y++)
            {
                // Создаем экземпляр префаба на нужных координатах
                Instantiate(prefab, new Vector3(x * _xSize - _x * _xSize / 2, y * _ySize - _y * _ySize / 2, 0),
                    Quaternion.identity, transform);
            }
        }

        _previousX = _x; // Сохраняем текущее значение _x
        _previousY = _y; // Сохраняем текущее значение _y
        _isLandscapeGenerated = true; // Устанавливаем флаг, что ландшафт сгенерирован
    }

    private void Update()
    {
        if (PlayerOutOfBoundaries()) // Проверяем, вышел ли игрок за границы ландшафта
        {
            ResetLandscape(); // Если да, сбрасываем ландшафт
        }
    }

    private bool PlayerOutOfBoundaries()
    {
        // Проверяем условие, когда игрок вышел за границу ландшафта
        Vector3 playerPosition = _playerTransform.position;
        return playerPosition.x < -_x * _xSize / 2 || playerPosition.x > _x * _xSize / 2 ||
               playerPosition.y < -_y * _ySize / 2 || playerPosition.y > _y * _ySize / 2;
    }

    private void ResetLandscape()
    {
        // Удаляем все сгенерированные объекты пола
        // foreach (Transform child in transform)
        // {
        //     Destroy(child.gameObject);
        // }

        // Перемещаем игрока на начальную позицию
        _playerTransform.position = _playerStartPosition;

        // Генерируем ландшафт заново
        // GenerateLandscape();
    }
}
