using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelDarkener : MonoBehaviour
{
    public float targetIntensity = 0.5f; // Целевая интенсивность освещения
    public float transitionDuration = 5f; // Длительность перехода между интенсивностями

    private float startIntensity; // Исходная интенсивность освещения
    private Light2D globalLight; // Ссылка на компонент Light2D для глобального освещения

    void Start()
    {
        GameObject globalLightObject = GameObject.Find("Global Light 2D");

        if (globalLightObject != null)
        {
            globalLight = globalLightObject.GetComponent<Light2D>();

            if (globalLight != null)
            {
                startIntensity = globalLight.intensity; // Сохраняем исходную интенсивность

                StartCoroutine(TransitionIntensity()); // Запускаем корутину для плавного перехода интенсивности
            }
            else
            {
                Debug.LogWarning("Global Light 2D не содержит компонент Light2D.");
            }
        }
        else
        {
            Debug.LogWarning("Не найден объект Global Light 2D.");
        }
    }

    private System.Collections.IEnumerator TransitionIntensity()
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / transitionDuration);

            globalLight.intensity = currentIntensity; // Изменяем интенсивность освещения

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        globalLight.intensity = targetIntensity; // Устанавливаем целевую интенсивность
    }
}
