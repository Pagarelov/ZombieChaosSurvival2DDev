using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public float rotateSpeed = 0.0025f;
    public float enemyHealth = 1f;
    public float distanceThreshold = 150f;

    private Rigidbody2D rb;
    public GameObject bulletPrefab;

    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;

    public float fireRate;
    private float timeToFire;

    public Transform firingPoint;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!target) 
        {
            GetTarget();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            RotateTowardsTarget(); 
        }

        if (target != null && (Mathf.Abs(target.position.x - transform.position.x) > distanceThreshold || Mathf.Abs(target.position.y - transform.position.y) > distanceThreshold))
        {
            Destroy(gameObject);
        }

        if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot) 
        {
            Shoot();
        }
    }

    private void Shoot() 
    {
        if (timeToFire <= 0f) 
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            timeToFire = fireRate;
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
            rb.velocity = transform.up * moveSpeed;    
            } 
            else 
            {
                rb.velocity = Vector2.zero;
            }
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
            // Destroy(other.gameObject); if player has one HP challenge
        } else if (other.gameObject.CompareTag("Bullet")) {
            enemyHealth--;
            if (enemyHealth <= 0) {
                LevelManager.manager.IncreaseScore(1);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}