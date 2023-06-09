using UnityEngine;
using TMPro;

public class DeadText : MonoBehaviour
{
    public TMP_Text deadText;

    public string[] deadTexts = {
        "Игра закончена",
        "Вы умерли",
        "",
        "Повезет в следующий раз",
        "Попробуй еще раз",
        "Лол, ты умер...",
        "тебя съели U_U",
        "",
        "Проблема с навыком",
        "Ты проиграл",
        "AAAAAAAoaoaoaoaoa",
        "О неееет",
        "RIP",
        "Упс, ты мертв",
        "Бро, тебе нужно тренероваться",
        "Миссия провалена",
        "Ой ой",
        "Требуется перезагрузка",
        "Нет больше шансов",
        "",
        "Вам бан!",
        "выпал из мира",
        "ты умер из-за зомби",
        "Поражение",
        "",
        "Прощай, смелая душа",
        "Перезапустить или выйти?",
        "Еще один повержен в прах",
        "Перманентная смерть"
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
