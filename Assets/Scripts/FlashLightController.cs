using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLightController : MonoBehaviour
{
    public float delay = 5f; // Задержка перед включением фонарика
    public float newIntensity = 1f; // Новая интенсивность фонарика
    public float transitionDuration = 2f; // Длительность перехода интенсивности фонарика

    public AudioClip flashlightSound; // Звуковой эффект при включении фонарика

    private Light2D flashlight; // Ссылка на компонент фонарика
    private float initialIntensity; // Исходная интенсивность фонарика
    private AudioSource audioSource; // Компонент для проигрывания звука

    private void Start()
    {
        flashlight = GetComponent<Light2D>(); // Получаем ссылку на компонент фонарика
        initialIntensity = flashlight.intensity; // Запоминаем исходную интенсивность фонарика
        audioSource = GetComponent<AudioSource>(); // Получаем ссылку на компонент проигрывания звука
        StartCoroutine(EnableFlashlightAfterDelay()); // Запускаем корутину для включения фонарика после задержки
    }

    private System.Collections.IEnumerator EnableFlashlightAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Ждем заданную задержку

        audioSource.PlayOneShot(flashlightSound); // Проигрываем звуковой эффект включения фонарика

        float elapsedTime = 0f; // Прошедшее время
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration; // Прогресс перехода
            flashlight.intensity = Mathf.Lerp(initialIntensity, newIntensity, t); // Применяем плавный переход интенсивности фонарика
            elapsedTime += Time.deltaTime; // Увеличиваем прошедшее время
            yield return null; // Ждем следующий кадр
        }

        flashlight.intensity = newIntensity; // Устанавливаем конечную интенсивность фонарика
    }
}
