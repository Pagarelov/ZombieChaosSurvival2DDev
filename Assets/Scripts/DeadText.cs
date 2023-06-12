using UnityEngine;
using TMPro;

public class DeadText : MonoBehaviour
{
    public TMP_Text deadText;

    public string[] deadTexts = {
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
        "Tuturu~",
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
        if (deadTexts.Length > 0)
        {
            int randomIndex = Random.Range(0, deadTexts.Length);
            deadText.text = "" + deadTexts[randomIndex];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.manager.GameOver();
        }
    }
}
