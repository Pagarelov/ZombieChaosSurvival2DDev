using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public float rotateSpeed = 0.0025f;
    public float enemyHealth = 1f;
    public float distanceThreshold = 150f;
    private Rigidbody2D rb;
    public AudioClip[] deathEnemySounds;

    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        GetTarget();
    }

    private void Update()
    {
        if (target) 
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance > distanceThreshold)
            {
                Destroy(gameObject);
                return;
            }
            
            RotateTowardsTarget(); 
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate() 
    {
        if (target) 
        {
            rb.velocity = transform.up * moveSpeed;
        }
    }

    private void RotateTowardsTarget() 
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void GetTarget() 
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) {
            target = player.transform;
        } 
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {

        } else if (other.gameObject.CompareTag("Bullet")) {
            enemyHealth--;
            if (enemyHealth <= 0) {
                LevelManager.manager.IncreaseScore(1);
                int randomIndex = Random.Range(0, deathEnemySounds.Length);
                AudioSource.PlayClipAtPoint(deathEnemySounds[randomIndex], transform.position);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}