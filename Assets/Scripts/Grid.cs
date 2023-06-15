using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Transform prefab;
    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    void Start()
    {
        for (int x = 0; x < _x; x++)
        {
            for (int y = 0; y < _y; y++)
            {
                Instantiate(prefab, new Vector3(x * _xSize - _x * _xSize / 2, y * _ySize - _y * _ySize /2, 0),
                    Quaternion.identity,transform);
            }
        }
    }
}
