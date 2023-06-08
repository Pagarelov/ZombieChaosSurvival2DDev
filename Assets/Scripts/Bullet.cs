using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range (1, 50)]
    [SerializeField] private float speed = 20f;

    [Range (1, 10)]
    [SerializeField] private float lifeTime = 3f;
    public HealthBar healthBar;

    private Rigidbody2D rb;
    
    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy (gameObject, lifeTime);
    } 

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() {
        rb.velocity = transform.up * speed;
    }
}
