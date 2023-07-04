using UnityEngine;
using TMPro;

public class DeadText : MonoBehaviour
{
    public TMP_Text deadText; // Ссылка на компонент TMP_Text для отображения текста

    public string[] deadTexts = { // Массив строк с различными текстами для отображения
        "Игра закончена",
        "Вы умерли",
        "Попробуйте еще раз",
        "Повезет в следующий раз",
        "Игра окончена",
        "Лол, ты умер...",
        "тебя съели U_U",
        "Бро, тебе нужно качаться",
        "Проблема с навыками",
        "Ты проиграл(а)",
        "AAAAAAAoaoaoaoaoa",
        "О неееет",
        "RIP",
        "Упс, ты мертв",
        "Повторим?",
        "Миссия провалена",
        "Требуется перезагрузка",
        "Нет больше шансов",
        "Tutturuu~",
        "Вам бан!",
        "выпал из мира",
        "ты умер из-за зомби",
        "Поражение",
        "DHTUwU передает вам привет",
        "Прощай, смелая душа",
        "Перезапустить или выйти?",
        "Еще один повержен в прах",
        "Перманентная смерть активирована"
        };
    private void Start()
    {
        if (deadTexts.Length > 0) // Проверяем, что массив текстов не пуст
        {
            int randomIndex = Random.Range(0, deadTexts.Length); // Генерируем случайный индекс в пределах размера массива
            deadText.text = "" + deadTexts[randomIndex]; // Устанавливаем случайный текст из массива в компонент TMP_Text
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Если объект, с которым столкнулись, имеет тег "Player"
        {
            LevelManager.manager.GameOver(); // Вызываем метод GameOver() у объекта LevelManager для завершения игры
        }
    }
}
