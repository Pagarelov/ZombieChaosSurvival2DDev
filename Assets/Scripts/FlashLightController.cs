using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLightController : MonoBehaviour
{
    public float delay = 5f; 
    public float newIntensity = 1f; 
    public float transitionDuration = 2f; 

    public AudioClip flashlightSound; 

    private Light2D flashlight;
    private float initialIntensity;
    private AudioSource audioSource;

    private void Start()
    {
        flashlight = GetComponent<Light2D>();
        initialIntensity = flashlight.intensity;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(EnableFlashlightAfterDelay());
    }

    private System.Collections.IEnumerator EnableFlashlightAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        audioSource.PlayOneShot(flashlightSound);

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
