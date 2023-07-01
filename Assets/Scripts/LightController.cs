using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private float delay = 5f;
    private float newIntensity = 1f;
    private float transitionDuration = 2f; 

    private Light2D flashlight;
    private float initialIntensity;

    private void Start()
    {
        flashlight = GetComponent<Light2D>();
        initialIntensity = flashlight.intensity;
        StartCoroutine(EnableFlashlightAfterDelay());
    }

    private System.Collections.IEnumerator EnableFlashlightAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            flashlight.intensity = Mathf.Lerp(initialIntensity, newIntensity, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        flashlight.intensity = newIntensity;
    }
}