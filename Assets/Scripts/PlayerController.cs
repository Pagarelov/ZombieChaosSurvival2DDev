using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10f; // Скорость перемещения игрока
    public float healthAmount = 100; // Количество здоровья игрока
    public Rigidbody2D rb; // Rigidbody игрока
    public Weapon weapon; // Оружие игрока
    public HealthBar healthBar; // Панель здоровья игрока
    public AudioClip[] fireSounds; // Звуки выстрела
    public AudioClip[] deathSounds; // Звуки смерти

    public float FireRate { get; set; } = 0.2f; // Частота выстрелов игрока
    private float nextFireTime = 0f; // Время следующего выстрела
    private bool isFiring = false; // Флаг состояния выстрела
    private bool isDead = false; // Флаг состояния смерти

    private Vector2 moveDirection; // Направление перемещения игрока
    private Vector2 mousePosition; // Положение указателя мыши

    private AudioSource audioSource; // Компонент AudioSource для проигрывания звуков

    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>(); // Получаем компонент оружия
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // Получаем ввод по горизонтали
        float moveY = Input.GetAxisRaw("Vertical"); // Получаем ввод по вертикали

        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true; // Запускаем выстрел, если нажата кнопка мыши
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isFiring = false; // Останавливаем выстрел, если кнопка мыши отпущена
        }

        if (isFiring && Time.time >= nextFireTime)
        {
            FireWeapon(); // Выстреливаем, если флаг выстрела активен и наступило время следующего выстрела
        }

        moveDirection = new Vector2(moveX, moveY).normalized; // Нормализуем направление перемещения игрока
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем положение указателя мыши в мировых координатах

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Die(); // Завершаем игру, если нажата клавиша Escape
        }
    }

    private void FixedUpdate()
    {
        Vector2 aimDirection = mousePosition - rb.position; // Получаем направление прицеливания
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f; // Вычисляем угол прицеливания
        transform.rotation = Quaternion.Euler(0, 0, aimAngle); // Устанавливаем вращение игрока в соответствии с прицеливанием

        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime); // Перемещаем игрока

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Обновляем положение указателя мыши в мировых координатах
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDead) return; // Игрок мертв, выходим из метода

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            healthBar.TakeDamage(2); // Игрок получает урон от пули врага
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            healthBar.TakeDamage(3); // Игрок получает урон от столкновения с врагом
        }

        if (healthAmount <= 0)
        {
            Die(); // Игрок умирает, если его здоровье достигает нуля
        }
    }

    private void FireWeapon()
    {
        weapon.Fire(FireRate, nextFireTime); // Выстреливаем оружием игрока
        nextFireTime = Time.time + FireRate; // Обновляем время следующего выстрела

        if (fireSounds != null && fireSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, fireSounds.Length);
            audioSource.PlayOneShot(fireSounds[randomIndex]); // Проигрываем случайный звук выстрела
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        LevelManager.manager.GameOver(); // Завершаем игру
        Destroy(gameObject); // Уничтожаем игрока
        int randomIndex = Random.Range(0, deathSounds.Length);
        AudioSource.PlayClipAtPoint(deathSounds[randomIndex], transform.position); // Проигрываем случайный звук смерти игрока
    }
}
