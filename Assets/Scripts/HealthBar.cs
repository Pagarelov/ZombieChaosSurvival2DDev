using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar; // Ссылка на изображение health bar
    public PlayerController playerController; // Ссылка на скрипт PlayerController

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Находим объект с PlayerController и получаем ссылку на его компонент
        if (playerController != null)
        {
            float healthAmount = playerController.healthAmount; // Получаем текущее значение здоровья из PlayerController
            healthBar.fillAmount = healthAmount / 100f; // Устанавливаем fillAmount health bar в соответствии с текущим здоровьем
        }
        else
        {
            Debug.LogWarning("PlayerController reference is not set."); // Выводим предупреждение, если ссылка на PlayerController не установлена
        }
    }

    public void TakeDamage(float damage)
    {
        if (playerController != null)
        {
            playerController.healthAmount -= damage; // Вычитаем полученный урон из здоровья игрока
            float healthAmount = playerController.healthAmount; // Получаем обновленное значение здоровья
            healthBar.fillAmount = healthAmount / 100f; // Обновляем fillAmount health bar в соответствии с обновленным здоровьем
        }
        else
        {
            Debug.LogWarning("PlayerController script not found."); // Выводим предупреждение, если скрипт PlayerController не найден
        }
    }
}
