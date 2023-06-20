using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour, IPointerClickHandler
{
    public AudioClip[] buttonSounds;
    public AudioSource audioSource;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        StartCoroutine(ChangeSceneWithDelay());
    }

    private System.Collections.IEnumerator ChangeSceneWithDelay()
    {
        if (buttonSounds != null && buttonSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, buttonSounds.Length);
            AudioClip randomSound = buttonSounds[randomIndex];
            audioSource.PlayOneShot(randomSound);
        }

        yield return new WaitForSeconds(4f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
