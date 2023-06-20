using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float healthAmout = 100;
    public Rigidbody2D rb;
    public Weapon weapon;
    public HealthBar healthBar;
    public AudioClip[] fireSounds;
    public AudioClip[] deathSounds;

    public float fireRate = 0.2f;
    private float nextFireTime = 0f;
    private bool isFiring = false;

    Vector2 moveDirection;
    Vector2 mousePosition;

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
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

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
            weapon.Fire(fireRate, nextFireTime);
            nextFireTime = Time.time + fireRate;

            if (fireSounds != null && fireSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, fireSounds.Length);
                audioSource.PlayOneShot(fireSounds[randomIndex]);
            }
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            healthBar.TakeDamage(2);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            healthBar.TakeDamage(3);
        }

        if (healthAmout <= 0)
        {
            LevelManager.manager.GameOver();
            Destroy(gameObject);
            int randomIndex = Random.Range(0, deathSounds.Length);
            AudioSource.PlayClipAtPoint(deathSounds[randomIndex], transform.position);
        }
    }
}
