using UnityEngine;
using TMPro;

public class DeadText : MonoBehaviour
{
    public TMP_Text deadText;

    public string[] deadTexts = { 
        "Game Over",
        "You died",
        "Try again",
        "Better luck next time",
        "L",
        "LoL, you died...",
        "тебя съели U_U",
        "L + ratio bozo",
        "Skill issue",
        "You lost",
        "AAAAAAAoaoaoaoaoa",
        "Oh nooooo",
        "RIP",
        "Oops, you're dead",
        "Out of lives",
        "Mission failed",
        "Restart required",
        "No more chances",
        "End of the line",
        "Вам бан!",
        "fell out of the world",
        "you died because of zombie",
        "Defeat",
        "you died from dehydration whilst trying to escape of zombie",
        "Farewell, brave soul",
        "Restart or quit?",
        "Another one bites the dust",
        "Permadeath activated"   
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
