using UnityEngine;

public class LandscapeGenerator : MonoBehaviour
{
    [SerializeField] private Transform prefab;
    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private Transform _playerTransform;
    private bool _isLandscapeGenerated;
    private int _previousX;
    private int _previousY;
    private Vector3 _playerStartPosition;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerStartPosition = _playerTransform.position;
        GenerateLandscape();
    }

    private void GenerateLandscape()
    {
        if (_isLandscapeGenerated)
        {
            return;
        }

        for (int x = 0; x < _x; x++)
        {
            for (int y = 0; y < _y; y++)
            {
                Instantiate(prefab, new Vector3(x * _xSize - _x * _xSize / 2, y * _ySize - _y * _ySize / 2, 0),
                    Quaternion.identity, transform);
            }
        }
        _previousX = _x;
        _previousY = _y;
        _isLandscapeGenerated = true;
    }

    private void Update()
    {
        // Проверка, если игрок вышел за границу по оси X или Y
        if (PlayerOutOfBoundaries())
        {
            ResetLandscape();
        }
    }

    private bool PlayerOutOfBoundaries()
    {
        // Проверка условия, когда игрок вышел за границу
        Vector3 playerPosition = _playerTransform.position;
        return playerPosition.x < -_x * _xSize / 2 || playerPosition.x > _x * _xSize / 2 ||
               playerPosition.y < -_y * _ySize / 2 || playerPosition.y > _y * _ySize / 2;
    }

    private void ResetLandscape()
    {
        // Уничтожаем все сгенерированные объекты пола
        // foreach (Transform child in transform)
        // {
        //     Destroy(child.gameObject);
        // }

        // Перемещаем игрока на начальную позицию
        _playerTransform.position = _playerStartPosition;

        // Генерируем пол заново
        // GenerateLandscape();
    }
}
