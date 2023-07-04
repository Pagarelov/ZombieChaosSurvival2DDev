using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target; // Цель, к которой будет двигаться враг
    public float moveSpeed; // Скорость движения врага
    public float rotateSpeed = 0.0025f; // Скорость поворота врага
    public float enemyHealth = 1f; // Здоровье врага
    public float distanceThreshold = 150f; // Пороговое расстояние для уничтожения врага
    private Rigidbody2D rb; // Ссылка на компонент Rigidbody2D
    public AudioClip[] deathEnemySounds; // Звуки смерти врага

    private AudioSource audioSource; // Компонент AudioSource для проигрывания звуков

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D из объекта
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource из объекта
        GetTarget(); // Получаем цель для врага
    }

    private void Update()
    {
        if (target) 
        {
            float distance = Vector2.Distance(transform.position, target.position); // Расстояние между врагом и целью
            if (distance > distanceThreshold) // Если расстояние превышает пороговое значение
            {
                Destroy(gameObject); // Уничтожаем врага
                return;
            }
            
            RotateTowardsTarget(); // Поворачиваем врага в сторону цели
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); // Двигаем врага к цели
        }
    }

    private void FixedUpdate() 
    {
        if (target) 
        {
            rb.velocity = transform.up * moveSpeed; // Задаем скорость движения врага в направлении вперед
        }
    }

    private void RotateTowardsTarget() 
    {
        Vector2 targetDirection = target.position - transform.position; // Направление к цели
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f; // Вычисляем угол поворота
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle)); // Создаем кватернион поворота
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed); // Применяем плавный поворот врага
    }

    private void GetTarget() 
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Находим игрока по тегу "Player"
        if (player) {
            target = player.transform; // Устанавливаем игрока как цель врага
        } 
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Действия при столкновении с игроком
        } else if (other.gameObject.CompareTag("Bullet")) {
            enemyHealth--; // Уменьшаем здоровье врага
            if (enemyHealth <= 0) { // Если здоровье врага меньше или равно 0
                LevelManager.manager.IncreaseScore(1); // Увеличиваем счет игры на 1
                int randomIndex = Random.Range(0, deathEnemySounds.Length); // Генерируем случайный индекс для звука смерти
                AudioSource.PlayClipAtPoint(deathEnemySounds[randomIndex], transform.position); // Проигрываем звук смерти врага
                Destroy(other.gameObject); // Уничтожаем объект пули
                Destroy(gameObject); // Уничтожаем врага
            }
        }
    }
}
