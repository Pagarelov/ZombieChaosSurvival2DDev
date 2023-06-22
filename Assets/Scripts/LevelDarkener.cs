using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelDarkener : MonoBehaviour
{
    public float targetIntensity = 0.5f;
    public float transitionDuration = 5f;

    private float startIntensity;
    private Light2D globalLight;

    void Start()
    {
        GameObject globalLightObject = GameObject.Find("Global Light 2D");

        if (globalLightObject != null)
        {
            globalLight = globalLightObject.GetComponent<Light2D>();

            if (globalLight != null)
            {
                startIntensity = globalLight.intensity;

                StartCoroutine(TransitionIntensity());
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

            globalLight.intensity = currentIntensity;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        globalLight.intensity = targetIntensity;
    }
}
