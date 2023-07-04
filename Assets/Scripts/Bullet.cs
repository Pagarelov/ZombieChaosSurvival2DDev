using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range (1, 50)] // Диапазон для скорости снаряда
    [SerializeField] private float speed = 20f; // Скорость снаряда

    [Range (1, 10)] // Диапазон для времени жизни снаряда
    [SerializeField] private float lifeTime = 3f; // Время жизни снаряда

    private Rigidbody2D rb; // Ссылка на компонент Rigidbody2D

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D из объекта
        Destroy(gameObject, lifeTime); // Уничтожаем объект снаряда через указанное время жизни
    } 

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player")) // Если столкнулись с объектом, помеченным тегом "Player"
        {
            Destroy(gameObject); // Уничтожаем объект снаряда
        }
    }

    private void FixedUpdate() {
        rb.velocity = transform.up * speed; // Задаем скорость снаряда в направлении вперед
    }
}
