using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public PlayerController playerController;

    private void Start() {
        playerController = playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            float healthAmount = playerController.healthAmout; 
            healthBar.fillAmount = healthAmount / 100f;
        }
        else
        {
            Debug.LogWarning("PlayerController reference is not set.");
        }
    }

    private void Update() {
        
    }

    public void TakeDamage(float damage)
    {
        if (playerController != null)
        {
            playerController.healthAmout -= damage; 
            float healthAmount = playerController.healthAmout; 
            healthBar.fillAmount = healthAmount / 100f;
        }
        else
        {
            Debug.LogWarning("PlayerController script not found.");
        }
    }
}