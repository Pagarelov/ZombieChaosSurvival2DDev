using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private float delay = 5f; // Задержка перед включением фонарика
    private float newIntensity = 1f; // Новая интенсивность фонарика
    private float transitionDuration = 2f; // Продолжительность перехода интенсивности

    private Light2D flashlight; // Компонент фонарика
    private float initialIntensity; // Исходная интенсивность фонарика

    private void Start()
    {
        flashlight = GetComponent<Light2D>(); // Получаем компонент фонарика
        initialIntensity = flashlight.intensity; // Сохраняем исходную интенсивность фонарика
        StartCoroutine(EnableFlashlightAfterDelay()); // Запускаем корутину для включения фонарика после задержки
    }

    private System.Collections.IEnumerator EnableFlashlightAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Ждем заданную задержку

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            flashlight.intensity = Mathf.Lerp(initialIntensity, newIntensity, t); // Интерполируем интенсивность фонарика
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        flashlight.intensity = newIntensity; // Устанавливаем новую интенсивность
    }
}
