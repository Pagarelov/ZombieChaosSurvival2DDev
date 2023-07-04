using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform target; // Цель, к которой преследует враг
    public float moveSpeed; // Скорость перемещения врага
    public float rotateSpeed = 0.0025f; // Скорость поворота врага
    public float enemyHealth = 1f; // Здоровье врага
    public float distanceThreshold = 150f; // Предел дистанции, при достижении которого враг уничтожается

    private Rigidbody2D rb; // Rigidbody врага
    public GameObject bulletPrefab; // Префаб пули врага

    public float distanceToShoot = 5f; // Дистанция, на которой враг начинает стрелять
    public float distanceToStop = 3f; // Дистанция, на которой враг останавливается

    public float fireRate; // Частота стрельбы
    private float timeToFire; // Время до следующего выстрела

    public Transform firingPoint; // Точка, откуда вылетают пули
    public AudioClip[] deathEnemySounds; // Звуки смерти врага

    private AudioSource audioSource; // Компонент AudioSource для проигрывания звуков

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource
    }

    private void Update()
    {
        if (!target) 
        {
            GetTarget(); // Получаем цель, если ее нет
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); // Перемещаемся к цели
            RotateTowardsTarget(); // Поворачиваемся к цели
        }

        if (target != null && (Mathf.Abs(target.position.x - transform.position.x) > distanceThreshold || Mathf.Abs(target.position.y - transform.position.y) > distanceThreshold))
        {
            Destroy(gameObject); // Если цель находится за пределами дистанции, уничтожаем врага
        }

        if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot) 
        {
            Shoot(); // Стреляем, если цель находится в пределах дистанции для стрельбы
        }
    }

    private void Shoot() 
    {
        if (timeToFire <= 0f) 
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation); // Создаем пулю
            timeToFire = fireRate; // Обновляем время до следующего выстрела
        } 
        else 
        {
            timeToFire -= Time.deltaTime;
        }
    }

    private void FixedUpdate() 
    {   
        if (target != null) 
        {
            if (Vector2.Distance(target.position, transform.position) >= distanceToStop)  {
            rb.velocity = transform.up * moveSpeed;    // Двигаем врага вперед
            } 
            else 
            {
                rb.velocity = Vector2.zero; // Останавливаем врага
            }
        }
    }

    private void RotateTowardsTarget() 
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed); // Поворачиваем врага к цели с использованием плавного вращения
    }

    private void GetTarget() 
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) {
            target = player.transform; // Находим игрока и устанавливаем его как цель
        } 
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Destroy(other.gameObject); if player has one HP challenge
        } else if (other.gameObject.CompareTag("Bullet")) {
            enemyHealth--; // Уменьшаем здоровье врага при попадании пули
            if (enemyHealth <= 0) {
                LevelManager.manager.IncreaseScore(1); // Увеличиваем счетчик очков
                int randomIndex = Random.Range(0, deathEnemySounds.Length);
                AudioSource.PlayClipAtPoint(deathEnemySounds[randomIndex], transform.position); // Проигрываем случайный звук смерти врага
                Destroy(other.gameObject); // Уничтожаем пулю
                Destroy(gameObject); // Уничтожаем врага
            }
        }
    }
}
