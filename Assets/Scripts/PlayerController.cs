using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10f;
    public float healthAmount = 100;
    public Rigidbody2D rb;
    public Weapon weapon;
    public HealthBar healthBar;
    public AudioClip[] fireSounds;
    public AudioClip[] deathSounds;

    public float FireRate { get; set; } = 0.2f;
    private float nextFireTime = 0f;
    private bool isFiring = false;
    private bool isDead = false;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    private float moveX;
    private float moveY;

    private AudioSource audioSource;

    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }

        if (isFiring && Time.time >= nextFireTime)
        {
            FireWeapon();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);

        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDead) return; // Игрок мертв, выходим из метода

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            healthBar.TakeDamage(2);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            healthBar.TakeDamage(3);
        }

        if (healthAmount <= 0)
        {
            Die();
        }
    }

    private void FireWeapon()
    {
        weapon.Fire(FireRate, nextFireTime);
        nextFireTime = Time.time + FireRate;

        if (fireSounds != null && fireSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, fireSounds.Length);
            audioSource.PlayOneShot(fireSounds[randomIndex]);
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        LevelManager.manager.GameOver();
        Destroy(gameObject);
        int randomIndex = Random.Range(0, deathSounds.Length);
        AudioSource.PlayClipAtPoint(deathSounds[randomIndex], transform.position);
    }
}
