using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour, IPointerClickHandler
{
    public AudioClip[] buttonSounds; // Звуки для кнопок
    public AudioSource audioSource; // Источник звука

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        StartCoroutine(ChangeSceneWithDelay()); // Запускаем сопрограмму с задержкой перед сменой сцены
    }

    private System.Collections.IEnumerator ChangeSceneWithDelay()
    {
        if (buttonSounds != null && buttonSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, buttonSounds.Length); // Выбираем случайный индекс из массива звуков
            AudioClip randomSound = buttonSounds[randomIndex]; // Получаем случайный звук
            audioSource.PlayOneShot(randomSound); // Проигрываем случайный звук
        }

        yield return new WaitForSeconds(4f); // Ждем 4 секунды

        UnityEngine.SceneManagement.SceneManager.LoadScene(1); // Загружаем сцену с индексом 1
    }
}
