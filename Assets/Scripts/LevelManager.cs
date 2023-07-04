using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager manager;

    public GameObject deathScreen; // Ссылка на объект экрана смерти
    public TextMeshProUGUI scoreText; // Ссылка на текстовое поле для отображения счета
    public TextMeshProUGUI scoreTextEnd; // Ссылка на текстовое поле для отображения счета на экране смерти
    public TextMeshProUGUI HighscoreText; // Ссылка на текстовое поле для отображения рекорда
    public TextMeshProUGUI SurvivalTimeText; // Ссылка на текстовое поле для отображения времени выживания

    public SaveData data; // Сохраненные данные

    public float survivalTime; // Время выживания
    private float currentTime; // Текущее время

    public int score; // Счет

    private void Update()
    {
        currentTime += Time.deltaTime;
        UpdateScoreText();
    }

    private void Awake()
    {
        manager = this;
        SaveSystem.Initialize();

        data = new SaveData(0);
        currentTime = 0f;
    }

    public IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(3f);
        deathScreen.SetActive(true);

        scoreText.text = "Счет: " + score.ToString();
        scoreTextEnd.text = "Счет: " + score.ToString();

        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }

        if (data.highscore < score)
        {
            data.highscore = score;
        }
        HighscoreText.text = "Рекорд: " + data.highscore.ToString();

        string formattedTime = string.Format("{0:0}{1:00}", Mathf.Floor(currentTime / 60), currentTime % 60);
        SurvivalTimeText.text = "Время выживания: " + formattedTime;

        string saveData = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveData);
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOver());
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Счет: " + score.ToString();
    }
}

[System.Serializable]
public class SaveData
{
    public int highscore;

    public SaveData(int _hs)
    {
        highscore = _hs;
    }
}
